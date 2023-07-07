using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using simple_rest.domain.models;

namespace simple_rest.framework.error;

public class Error
{
    public static IActionResult StatusError(Exception err){
        Response res = new Response();
        res.code = (int)StatusCodes.Status500InternalServerError;
        res.message = err.Message;
        if (err is MySqlException){
            MySqlException? mySqlException = err as MySqlException;
            if (mySqlException?.Number == (int)MySqlErrorCode.DuplicateKeyEntry){
                res.code = (int)StatusCodes.Status400BadRequest;
            }
        }
        return new JsonResult(res){
            StatusCode = res.code
        };
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