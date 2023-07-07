using simple_rest.domain.query;
using simple_rest.domain.models;

namespace simple_rest.usecase;

public interface IUsecase
{
    IEnumerable<WeatherForecast> GetAllData();
    IEnumerable<WeatherForecast> GetDataById(int id);
    string Create(WeatherForecast req);
    string Update(WeatherForecast req);
    string Delete(int id);
}
public class Usecase : IUsecase{
    public Query Query { get; set; }
    public Usecase(Query query){
        Query = query;
    }

    public IEnumerable<WeatherForecast> GetAllData(){
        return Query.GetAllData();
    }

    public IEnumerable<WeatherForecast> GetDataById(int id){
        return Query.GetDataById(id);
    }

    public string Create(WeatherForecast req){
        return Query.Create(req);
    }

        public string Update(WeatherForecast req){
        return Query.Update(req);
    }
        public string Delete(int id){
        return Query.Delete(id);
    }
}