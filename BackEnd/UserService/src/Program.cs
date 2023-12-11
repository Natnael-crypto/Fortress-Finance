using MySqlConnector;
namespace UserService;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using UserService.Repositories;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthentication(options => 
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = "Bearer";
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            
        });
        builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);
        builder.Services.AddControllers();
        builder.Services.AddScoped<UserRepository>(provider =>
            {
                IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
                return new UserRepository(configuration);
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
