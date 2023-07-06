using simple_rest.domain.query;

namespace simple_rest.usecase;

public interface IUsecase
{
    IEnumerable<WeatherForecast> GetAllData();
}
public class Usecase : IUsecase{
    public Query Query { get; set; }
    public Usecase(Query query){
        Query = query;
    }

    public IEnumerable<WeatherForecast> GetAllData(){
        return Query.GetAllData();
    }
}