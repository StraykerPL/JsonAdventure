using JsonAdventure.Persistance.DependencyInjection;
using JsonAdventure.Application.DependencyInjection;

namespace JsonAdventure.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}