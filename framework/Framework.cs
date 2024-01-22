using simple_rest.framework.config;
using simple_rest.framework.database;
using simple_rest.domain.query;
using simple_rest.usecase;
using simple_rest.framework.error;
using Microsoft.Extensions.Logging.Console;


namespace simple_rest.framework;

public class Framework{
    public static void Run(){
        new Config("./config.json").Load();
        Database? database = null;
        try{
            database = new Database();
        }catch (Exception err){
            throw new Exception(err.Message);
        }
        Usecase useCase = new(database);
        Query query = new(database.ConnectMySQL());
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();
        builder.Services.AddSingleton<IError, Error>();
        builder.Services.AddSingleton<IDatabase, Database>();
        builder.Services.AddScoped<IProjectQuery, Query>(provider => query);
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