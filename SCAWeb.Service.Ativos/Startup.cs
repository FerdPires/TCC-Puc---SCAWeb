using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SCAWeb.Service.Ativos.Data;
using SCAWeb.Service.Ativos.Repositories;
using SCAWeb.Service.Ativos.Repositories.Interfaces;
using SCAWeb.Service.Ativos.Services;
using SCAWeb.Service.Ativos.Services.Interfaces;

namespace SCAWeb.Service.Ativos
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.AddDbContext<AtivosContext>(options => options.UseInMemoryDatabase("Database"));
            services.AddDbContext<AtivosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("connectionLocalDb")));

            services.AddTransient<IAgendaManutencaoRepository, AgendaManutencaoRepository>();
            services.AddTransient<IFornecedorRepository, FornecedorRepository>();
            services.AddTransient<IInsumoRepository, InsumoRepository>();
            services.AddTransient<IManutencaoRepository, ManutencaoRepository>();
            services.AddTransient<ITipoInsumoRepository, TipoInsumoRepository>();

            services.AddTransient<IAgendaManutencaoService, AgendaManutencaoService>();           
            services.AddTransient<IFornecedorService, FornecedorService>();           
            services.AddTransient<IInsumoService, InsumoService>();           
            services.AddTransient<IManutencaoService, ManutencaoService>();            
            services.AddTransient<ITipoInsumoService, TipoInsumoService>();
            
            services.AddTransient<AgendaManutencaoService, AgendaManutencaoService>();
            services.AddTransient<FornecedorService, FornecedorService>();
            services.AddTransient<InsumoService, InsumoService>();
            services.AddTransient<ManutencaoService, ManutencaoService>();
            services.AddTransient<TipoInsumoService, TipoInsumoService>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = "Bearer";
            })
            .AddJwtBearer("Bearer", x =>
            {
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["jwtTokenConfig:issuer"],
                    ValidAudience = Configuration["jwtTokenConfig:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtTokenConfig:secret"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"SCAWeb.Service.Ativos.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Serviço de Ativos",
                });
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ativos");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
