
using Company.Resources.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ResourcesWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            var resourceConnection = builder.Configuration.GetConnectionString("DefaultConnection");
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ResourceContext>(options =>
                  options.UseSqlServer(resourceConnection));
            var app = builder.Build();

            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseDefaultFiles(); // ищет index.html в wwwroot
            app.UseStaticFiles();  // разрешает отдавать статические файлы

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
