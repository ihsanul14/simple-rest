using Microsoft.AspNetCore.Mvc;
using simple_rest.usecase;
using System.Net;
using simple_rest.domain.models;
using simple_rest.framework.error;

namespace simple_rest.application.controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> Logger;
    private readonly IUsecase UseCase;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IUsecase usecase)
    {
        Logger = logger;
        UseCase = usecase;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get()
    {
        IEnumerable<WeatherForecast> res = UseCase.GetAllData();
        if (res.Count() == 0){
            return NotFound(new Response{
                code = (int)HttpStatusCode.NotFound,
                message = "no data found",
            });
        }
        return (OkObjectResult)Ok(new Response{
            code = (int)HttpStatusCode.OK,
            message = "success retrieve data",
            data = res,
        });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
        {
            try{
                IEnumerable<WeatherForecast> data = UseCase.GetDataById(id);
                Response res= new Response();
                res.code =(int)StatusCodes.Status200OK;
                res.message = $"success retrive data with id {id}";
                if (data.Count() == 0)
                {
                    res.code = (int)StatusCodes.Status404NotFound;
                    res.message = $"no data found with id {id}";
                    return NotFound(res);
                }else{
                    res.data = data;
                    return Ok(res);
                }
            } catch(Exception err){
                return Error.StatusError(err);
            }
        }
        
    [HttpPost]
        public IActionResult Create([FromBody] WeatherForecast req)
        {
            var validation = new WeatherForecastValidator.CreateWeatherForecastRequest(); 
            if (!validation.Validate(req).IsValid)
            {
                return BadRequest(new Response{
                code = (int)HttpStatusCode.BadRequest,
                message = new Error().GetValidationError(validation.Validate(req).Errors),
            });
            }
            try{
                string message = UseCase.Create(req);
                return Ok(new Response{
                    code = (int)StatusCodes.Status200OK,
                    message = message,
                });
            } catch(Exception err){
                return Error.StatusError(err);
            }
            
        }
        
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] WeatherForecast req, int id)
        {
            req.Id = id;
            var validation = new WeatherForecastValidator.UpdateWeatherForecastRequest(); 
            if (!validation.Validate(req).IsValid)
            {
                return BadRequest(new Response{
                code = (int)HttpStatusCode.BadRequest,
                message = new Error().GetValidationError(validation.Validate(req).Errors),
            });
            }
            try{
                string message = UseCase.Update(req);
                return Ok(new Response{
                    code = (int)StatusCodes.Status200OK,
                    message = message,
                });
            } catch(Exception err){
                return Error.StatusError(err);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var validation = new WeatherForecastValidator.DeleteWeatherForecastRequest();
            if (!validation.Validate(id).IsValid)
            {
                return BadRequest(new Response{
                code = (int)HttpStatusCode.BadRequest,
                message = new Error().GetValidationError(validation.Validate(id).Errors),
            });
            }
            try{
                string message = UseCase.Delete(id);
                return Ok(new Response{
                    code = (int)StatusCodes.Status200OK,
                    message = message,
                });
            } catch(Exception err){
                return Error.StatusError(err);
            }
        }
}
