
using Microsoft.EntityFrameworkCore;
using NLog;
using WebApplicationLogging.Loggings;
using WebApplicationLogging.Models;

namespace WebApplicationLogging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

            // Add services to the container.
            builder.Services.AddDbContext<NorthwindContext>(
            options => options.UseSqlServer("Data Source=khuong\\SQLEXPRESS03;Initial Catalog=Northwind;User ID=sa;Password=25122004;Encrypt=False"));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
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