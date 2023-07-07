using System.Text.Json.Serialization;
using FluentValidation;

namespace simple_rest.domain.models;

public class WeatherForecast
{
    public int Id { get; set; }

    public string? Nama { get; set; }

    public int? Nomor {get; set;}

    public DateTime? Created_at { get; set; }
    public DateTime? Updated_at { get; set; }
}

public class WeatherForecastValidator
{
    public class CreateWeatherForecastRequest: AbstractValidator<WeatherForecast>{
    public CreateWeatherForecastRequest()
    {
        RuleFor(x => x.Nama).NotEmpty();
        RuleFor(x => x.Nomor).GreaterThan(0);
    }
    }

    public class UpdateWeatherForecastRequest: AbstractValidator<WeatherForecast>{
    public UpdateWeatherForecastRequest()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Nama).NotEmpty();
        RuleFor(x => x.Nomor).GreaterThan(0);
    }
    }

    public class DeleteWeatherForecastRequest: AbstractValidator<int>{
    public DeleteWeatherForecastRequest()
    {
        RuleFor(x => x).NotEmpty().GreaterThan(0);
    }
    }
    
}

public class Response{
    public int code {get; set;}
    public string? message {get; set;}
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<WeatherForecast>? data {get; set;}
}
