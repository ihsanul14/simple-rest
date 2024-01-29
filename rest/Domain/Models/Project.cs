using System.Text.Json.Serialization;
using FluentValidation;

namespace rest.Domain.Models;

public class Project
{
    public int Id { get; set; }

    public string? Nama { get; set; }

    public int? Nomor {get; set;}

    public DateTime? Created_at { get; set; }
    public DateTime? Updated_at { get; set; }
}

public class ProjectValidator
{
    public class CreateProjectRequest: AbstractValidator<Project>{
    public CreateProjectRequest()
    {
        RuleFor(x => x.Nama).NotEmpty();
        RuleFor(x => x.Nomor).GreaterThan(0);
    }
    }

    public class UpdateProjectRequest: AbstractValidator<Project>{
    public UpdateProjectRequest()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Nama).NotEmpty();
        RuleFor(x => x.Nomor).GreaterThan(0);
    }
    }

    public class DeleteProjectRequest: AbstractValidator<int>{
    public DeleteProjectRequest()
    {
        RuleFor(x => x).NotEmpty().GreaterThan(0);
    }
    }
    
}

public class Response{
    public int code {get; set;}
    public string? message {get; set;}
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<Project>? data {get; set;}
}
