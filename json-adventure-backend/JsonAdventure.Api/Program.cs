using JsonAdventure.Api.Authentication;
using JsonAdventure.Application.DependencyInjection;
using JsonAdventure.Persistance.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

namespace JsonAdventure.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCors();
            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JsonAdventure",
                    Description = "ASP.NET Core Web API backend app to deliver simple RPG experience",
                });
                options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authentication header using the Bearer scheme."
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            string connectionString = builder.Configuration.GetConnectionString("MySql")!;
            string version = builder.Configuration.GetSection("MySqlConfig")["version"]!;
            builder.Services.AddMySqlDb(connectionString, version);

            builder.Services.AddRepositories();
            builder.Services.AddServices();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x =>
            {
                x.AllowCredentials();
                x.AllowAnyHeader();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}