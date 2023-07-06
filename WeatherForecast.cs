namespace simple_rest;

public class WeatherForecast
{
    public int Id { get; set; }

    public string? Nama { get; set; }

    public int Nomor {get; set;}

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static implicit operator List<object>(WeatherForecast v)
    {
        throw new NotImplementedException();
    }
}
