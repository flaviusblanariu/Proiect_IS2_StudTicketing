
using Microsoft.EntityFrameworkCore;
using StudTicketing.Database;
using StudTicketing.Services.Abstractions;
using StudTicketing.Services.Implementations;

namespace StudTicketing;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services
            .AddDbContext<
                TicketingDatabaseContext>(o =>
                o.UseSqlite("Datasource=../StudTicketing.db;")) // Adaugam in DI baza de date SQLite cu sursa sa
            .AddScoped<IUserService, UserService>();
        builder.Services.AddControllers();
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

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
