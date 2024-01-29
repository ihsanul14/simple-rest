using MySql.Data.MySqlClient;
using rest.Domain.Models;

namespace rest.Framework.ErrorFramework;

public interface IError{
    Response StatusError(Exception err);
    string GetValidationError(System.Collections.Generic.List<FluentValidation.Results.ValidationFailure> err);
}

public class Error : IError
{
    private readonly ILogger<Error> Logger;
    public Error(ILogger<Error> logger){
        Logger = logger;
    }
    public Response StatusError(Exception err){
        Logger.LogError(err.Message);
        Response res = new()
        {
            code = StatusCodes.Status500InternalServerError,
            message = err.Message
        };        
        if (err is MySqlException){
            MySqlException? mySqlException = err as MySqlException;
            if (mySqlException?.Number == (int)MySqlErrorCode.DuplicateKeyEntry){
                res.code = StatusCodes.Status400BadRequest;
            }
        }
        return res;
    }
    public string GetValidationError(System.Collections.Generic.List<FluentValidation.Results.ValidationFailure> err){
        string[] errorMessages = new string[err.Count];
        for (int i = 0; i < err.Count; i++)
        {
            errorMessages[i] = err[i].ErrorMessage;
        }
        return string.Join(",",errorMessages);
    }
}