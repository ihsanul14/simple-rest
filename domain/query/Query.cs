using MySql.Data.MySqlClient;

namespace simple_rest.domain.query;

public class Query{
    public MySqlConnection MysqlConnection { get; set; }
    public Query(MySqlConnection mySqlConnection){
        MysqlConnection = mySqlConnection;
    }

    public IEnumerable<WeatherForecast> GetAllData(){
        MysqlConnection.Open();
        string query = "SELECT * FROM testing";
        MySqlCommand command = new MySqlCommand(query, MysqlConnection);
        using (MySqlDataReader reader = command.ExecuteReader())
        {
            IEnumerable<WeatherForecast> results = new List<WeatherForecast>();
            while (reader.Read())
            {
                // Dictionary<string, object> row = Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue);
                // results.Add(row);
                Console.WriteLine(reader);
            }
            return results;
        }
    }
}