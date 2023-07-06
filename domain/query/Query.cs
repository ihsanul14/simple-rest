using MySql.Data.MySqlClient;

namespace simple_rest.domain.query;

public interface IQuery{
    IEnumerable<WeatherForecast> GetAllData();
}
public class Query : IQuery{
    public MySqlConnection MysqlConnection { get; set; }
    public Query(MySqlConnection mySqlConnection){
        MysqlConnection = mySqlConnection;
        MysqlConnection.Open();
    }


    public IEnumerable<WeatherForecast> GetAllData(){
        string query = "SELECT * FROM testing WHERE nomor";
        MySqlCommand command = new MySqlCommand(query, MysqlConnection);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            List<WeatherForecast> results = new List<WeatherForecast>();
            while (reader.Read())
            {
                // Console.WriteLine(reader["nomor"] == DBNull.Value ?);
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


}