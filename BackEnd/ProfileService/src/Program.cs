using Microsoft.EntityFrameworkCore;
using ProfileService.Data;
using ProfileService.Interfaces;
using ProfileService.Repositories;

namespace ProfileService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string _connectionString = builder.Configuration.GetConnectionString("Default")!; 

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
        builder.Services.AddDbContext<ProfileDataContext>(options =>
        {
           options.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString)); 
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        

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


