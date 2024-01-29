using rest.Domain.Query;
using rest.Domain.Models;
using System.Data.Entity;
using Database = rest.Framework.DatabaseFramework;

namespace rest.Usecase;

public interface IProjectUsecase
{
    IEnumerable<Project> GetAllData();
    IEnumerable<Project> GetDataById(int id);
    string Create(Project req);
    string Update(Project req);
    string Delete(int id);
}
public class ProjectUsecase : IProjectUsecase{

    public Query Query { get; set; }
    public ProjectUsecase(Query query){
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