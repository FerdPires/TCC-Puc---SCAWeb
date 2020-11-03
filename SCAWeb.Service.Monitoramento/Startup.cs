using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SCAWeb.Service.Monitoramento.Data;
using SCAWeb.Service.Monitoramento.Repositories;
using SCAWeb.Service.Monitoramento.Repositories.Interfaces;
using SCAWeb.Service.Monitoramento.Util.Interfaces;
using System;
using System.Text;

namespace SCAWeb.Service.Monitoramento
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

            //services.AddDbContext<MonitoramentoContext>(options => options.UseInMemoryDatabase("Database"));
            services.AddDbContext<MonitoramentoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("connectionLocalDb")));

            services.AddTransient<IAlertaSensorRepository, AlertaSensorRepository>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<ISensorRepository, SensorRepository>();

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
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"SCAWeb.Service.Monitoramento.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Serviço de Monitoramento de Barragens",
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

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Monitoramento");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
