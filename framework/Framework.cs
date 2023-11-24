using simple_rest.framework.config;
using simple_rest.framework.database;
using simple_rest.domain.query;
using simple_rest.usecase;
using System.Data;
using Microsoft.Extensions.Configuration;


namespace simple_rest.framework;

public class Framework{

    public static void Run(){
        Config.Load("./config.json");
        Database db = new();
        (IDbConnection? connection, Exception? err) = db.GetConnection();
        if (err != null){
            Console.WriteLine(err);
            Environment.Exit(1);
        }
        if (connection != null){
            Query query = new(connection);
            Usecase useCase = new(query);
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IDbConnection>(provider => connection);
            builder.Services.AddScoped<IWeatherQuery, Query>(provider => query);
            builder.Services.AddScoped<IUsecase, Usecase>(provider => useCase);
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