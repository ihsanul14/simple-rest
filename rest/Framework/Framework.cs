using rest.Framework.ConfigFramework;
using rest.Framework.DatabaseFramework;
using rest.Domain.Query;
using rest.Usecase;
using rest.Framework.ErrorFramework;
using Microsoft.Extensions.Logging.Console;


namespace rest.Framework;

public class FrameworkClass{
    public static void Run(){
        new Config("./config.json").Load();
        Database? database = null;
        try{
            database = new Database();
        }catch (Exception err){
            throw new Exception(err.Message);
        }
        Query query = new(database.ConnectMySQL());
        ProjectUsecase useCase = new(query);
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddControllers();
        builder.Services.AddSingleton<IError, Error>();
        builder.Services.AddSingleton<IDatabase, Database>();
        builder.Services.AddScoped<IProjectQuery, Query>(provider => query);
        builder.Services.AddScoped<IProjectUsecase, ProjectUsecase>(provider => useCase);
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