using MySql.Data.MySqlClient;
using NHibernate;
using simple_rest.domain.models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace simple_rest.domain.query;

public interface IProjectQuery{
    IEnumerable<Project> GetAllData();
    IEnumerable<Project> GetDataById(int id);
    string Create(Project req);
    string Update(Project req);
    string Delete(int id);
}
public class Query : IProjectQuery{
    public IDbConnection? Db { get; set; }
    public Query(IDbConnection? mySqlConnection){
        Db = mySqlConnection;
    }


    public IEnumerable<Project> GetAllData(){
        string query = "SELECT * FROM testing";
        return (IEnumerable<Project>)Db.Query<Project>(query);
    }

    public IEnumerable<Project> GetDataById(int id){
        string query = "SELECT * FROM testing WHERE id = @id";
        return (IEnumerable<Project>)Db.Query<Project>(query, new{Id= id});
    }

    public string Create(Project req){
        string query = """
            INSERT INTO testing (nama, nomor, created_at) VALUES (
                @nama, @nomor, now()
            )
        """;
        Db.Execute(query, req);
        return "success create data";
    }

    public string Update(Project req){
        var query = "UPDATE testing set nama = @nama, nomor = @nomor, updated_at = now() WHERE id = @id";
        Db.Execute(query, req);
        return $"success update data with id {req.Id}";
    }

    public string Delete(int id){
        string query = "DELETE from testing WHERE id = @id";
        Db.Execute(query, new {Id = id});
        return $"success delete data with id {id}";
    }

}