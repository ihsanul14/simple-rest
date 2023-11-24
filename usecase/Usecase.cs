using simple_rest.domain.query;
using simple_rest.domain.models;

namespace simple_rest.usecase;

public interface IUsecase
{
    IEnumerable<Project> GetAllData();
    IEnumerable<Project> GetDataById(int id);
    string Create(Project req);
    string Update(Project req);
    string Delete(int id);
}
public class Usecase : IUsecase{
    public Query Query { get; set; }
    public Usecase(Query query){
        Query = query;
    }

    public IEnumerable<Project> GetAllData(){
        return Query.GetAllData();
    }

    public IEnumerable<Project> GetDataById(int id){
        return Query.GetDataById(id);
    }

    public string Create(Project req){
        return Query.Create(req);
    }
        public string Update(Project req){
        return Query.Update(req);
    }
        public string Delete(int id){
        return Query.Delete(id);
    }
}