using simple_rest.framework.config;
using simple_rest.framework.database;
using MySql.Data.MySqlClient;
using simple_rest.domain.query;
using simple_rest.usecase;
using simple_rest.application.controllers;
using Microsoft.Extensions.DependencyInjection;

namespace simple_rest.framework;

public class Framework{
    private static readonly string[] args;

    public static void Run(){
        Config.Load("./.env");
        Database db = new Database();
        (MySqlConnection? connection, Exception? err) = db.GetConnection();
        if (err != null){
            Console.WriteLine(err);
            Environment.Exit(1);
        }
        if (connection != null){
            Query query = new Query(connection);
            Usecase useCase = new Usecase(query);
            // ILogger<WeatherForecastController> _logger = new ILogger<WeatherForecastController>()
            // WeatherForecastController controller = new WeatherForecastController(null, useCase);

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddScoped<IUsecase, Usecase>();
            builder.Services.AddSingleton(connection);
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
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