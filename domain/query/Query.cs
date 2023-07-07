using MySql.Data.MySqlClient;
using simple_rest.domain.models;
namespace simple_rest.domain.query;

public interface IQuery{
    IEnumerable<WeatherForecast> GetAllData();
    IEnumerable<WeatherForecast> GetDataById(int id);
    string Create(WeatherForecast req);
    string Update(WeatherForecast req);
    string Delete(int id);
}
public class Query : IQuery{
    public MySqlConnection MysqlConnection { get; set; }
    public Query(MySqlConnection mySqlConnection){
        MysqlConnection = mySqlConnection;
        MysqlConnection.Open();
    }


    public IEnumerable<WeatherForecast> GetAllData(){
        string query = "SELECT * FROM testing";
        MySqlCommand command = new MySqlCommand(query, MysqlConnection);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            List<WeatherForecast> results = new List<WeatherForecast>();
            while (reader.Read())
            {
                WeatherForecast row = new WeatherForecast{
                    Id = reader["id"] != DBNull.Value ? reader.GetInt32("id") : 0,
                    Nama = reader["nama"] != DBNull.Value ? reader.GetString("nama") : "",
                    Nomor = reader["nomor"] != DBNull.Value ? reader.GetInt32("nomor") : 0,
                    CreatedAt = reader["created_at"] != DBNull.Value ? reader.GetDateTime("created_at") : null,
                    UpdatedAt = reader["updated_at"] != DBNull.Value ? reader.GetDateTime("updated_at") : null,
                };
                results.Add(row);
            }
            return results;
        }
    }

    public IEnumerable<WeatherForecast> GetDataById(int id){
        string query = "SELECT * FROM testing Where id = @id";
        MySqlCommand command = new MySqlCommand(query, MysqlConnection);
        command.Parameters.AddWithValue("@id", id);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            List<WeatherForecast> results = new List<WeatherForecast>();
            while (reader.Read())
            {
                WeatherForecast row = new WeatherForecast{
                    Id = reader["id"] != DBNull.Value ? reader.GetInt32("id") : 0,
                    Nama = reader["nama"] != DBNull.Value ? reader.GetString("nama") : "",
                    Nomor = reader["nomor"] != DBNull.Value ? reader.GetInt32("nomor") : 0,
                    CreatedAt = reader["created_at"] != DBNull.Value ? reader.GetDateTime("created_at") : null,
                    UpdatedAt = reader["updated_at"] != DBNull.Value ? reader.GetDateTime("updated_at") : null,
                };
                results.Add(row);
            }
            return results;
        }
    }

    public string Create(WeatherForecast req){
        string query = """
            INSERT INTO testing (nama, nomor, created_at) VALUES (
                @nama, @nomor, now()
            )
        """;
        MySqlCommand command = new MySqlCommand(query, MysqlConnection);
        command.Parameters.AddWithValue("@nama", req.Nama);
        command.Parameters.AddWithValue("@nomor", req.Nomor);
        using (MySqlDataReader reader = command.ExecuteReader())
        return "success insert data";
    }

    public string Update(WeatherForecast req){
        string query = "UPDATE testing set nama = @nama, nomor = @nomor, updated_at = now() WHERE id = @id";
        MySqlCommand command = new MySqlCommand(query, MysqlConnection);
        command.Parameters.AddWithValue("@id", req.Id);
        command.Parameters.AddWithValue("@nama", req.Nama);
        command.Parameters.AddWithValue("@nomor", req.Nomor);
        using (MySqlDataReader reader = command.ExecuteReader())
        return $"success update data with id {req.Id}";
    }

    public string Delete(int id){
        string query = "DELETE from testing WHERE id = @id";
        MySqlCommand command = new MySqlCommand(query, MysqlConnection);
        command.Parameters.AddWithValue("@id", id);
        using (MySqlDataReader reader = command.ExecuteReader())
        return $"success delete data with id {id}";
    }

}