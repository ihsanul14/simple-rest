using Microsoft.AspNetCore.Mvc;
using rest.Usecase;
using System.Net;
using rest.Domain.Models;
using rest.Framework.ErrorFramework;


namespace rest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IError IError;
    private readonly IProjectUsecase UseCase;

    public ProjectController(IError error,IProjectUsecase usecase)
    {
        IError = error;
        UseCase = usecase;
    }

    [HttpGet(Name = "GetProject")]
    [ProducesResponseType(typeof(Response), 200)]
    public IActionResult Get()
    {
        try{
            IEnumerable<Project> res = UseCase.GetAllData();
            if (res?.Count() == 0){
                return NotFound(new Response{
                    code = (int)HttpStatusCode.NotFound,
                    message = "no data found",
                });
            }
            return Ok(new Response{
                code = (int)HttpStatusCode.OK,
                message = "success retrieve data",
                data = res,
            });
        }catch(Exception err){
            Response res = IError.StatusError(err);
            return new JsonResult(res){
                StatusCode = res.code
            };
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
        {
            try{
                IEnumerable<Project> data = UseCase.GetDataById(id);
                Response res = new()
                {
                    code = StatusCodes.Status200OK,
                    message = $"success retrive data with id {id}"
                };
                if (data?.Count() == 0)
                {
                    res.code = StatusCodes.Status404NotFound;
                    res.message = $"no data found with id {id}";
                    return NotFound(res);
                }else{
                    res.data = data;
                    return Ok(res);
                }
            } catch(Exception err){
                Response res = IError.StatusError(err);
                return new JsonResult(res){
                    StatusCode = res.code
                };
            }
        }
        
    [HttpPost]
        public IActionResult Create([FromBody] Project req)
        {
            var validation = new ProjectValidator.CreateProjectRequest(); 
            if (!validation.Validate(req).IsValid)
            {
                return BadRequest(new Response{
                code = (int)HttpStatusCode.BadRequest,
                message = IError.GetValidationError(validation.Validate(req).Errors),
            });
            }
            try{
                string message = UseCase.Create(req);
                return Ok(new Response{
                    code = StatusCodes.Status200OK,
                    message = message,
                });
            } catch(Exception err){
                Response res = IError.StatusError(err);
                return new JsonResult(res){
                    StatusCode = res.code
                };
            }
            
        }
        
        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Project req, int id)
        {
            req.Id = id;
            var validation = new ProjectValidator.UpdateProjectRequest(); 
            if (!validation.Validate(req).IsValid)
            {
                return BadRequest(new Response{
                code = (int)HttpStatusCode.BadRequest,
                message = IError.GetValidationError(validation.Validate(req).Errors),
            });
            }
            try{
                string message = UseCase.Update(req);
                return Ok(new Response{
                    code = StatusCodes.Status200OK,
                    message = message,
                });
            } catch(Exception err){
                Response res = IError.StatusError(err);
                return new JsonResult(res){
                    StatusCode = res.code
                };
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var validation = new ProjectValidator.DeleteProjectRequest();
            if (!validation.Validate(id).IsValid)
            {
                return BadRequest(new Response{
                code = (int)HttpStatusCode.BadRequest,
                message = IError.GetValidationError(validation.Validate(id).Errors),
            });
            }
            try{
                string message = UseCase.Delete(id);
                return Ok(new Response{
                    code = StatusCodes.Status200OK,
                    message = message,
                });
            } catch(Exception err){
                Response res = IError.StatusError(err);
                return new JsonResult(res){
                    StatusCode = res.code
                };
            }
        }
}
