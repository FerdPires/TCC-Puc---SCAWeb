using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Pedido
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
           // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //            var identityUrl = "https://localhost:44352";

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = "Bearer";
                //   x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //      x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("Bearer", x =>
            {
                //  x.Authority = identityUrl;
                x.RequireHttpsMetadata = false;
             //   x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["jwtTokenConfig:issuer"],
                    ValidAudience = Configuration["jwtTokenConfig:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtTokenConfig:secret"])),
                    //ValidateIssuerSigningKey = true,
                    ////IssuerSigningKey = new SymmetricSecurityKey(key),
                    //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    //ValidateIssuer = false,
                    //ValidAudiences = new[] { "pedido" }
                    //   ValidAudience = Configuration["Jwt:Issuer"],
                    ClockSkew = TimeSpan.Zero
                };

            });
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            //}).AddJwtBearer(options =>
            //{
            //    // options.Authority = identityUrl;
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
            //    options.RequireHttpsMetadata = false;
            //    options.Audience = "pedido";
            //});

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "AuthSchemeKey";

            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = identityUrl;
            //    options.RequireHttpsMetadata = false;
            //    options.Audience = "pedido";
            //});

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = "AuthSchemeKey";
            //})
            //.AddJwtBearer("AuthSchemeKey", options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = Configuration["Jwt:Issuer"],
            //        ValidAudience = Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"Pedido.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pedido API",
                });
            });
            #endregion

            services.AddControllers();
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pedido");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
