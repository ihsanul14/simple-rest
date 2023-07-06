using MySql.Data.MySqlClient;


namespace simple_rest.framework.database;

public class Database{
    public (MySqlConnection?,Exception?) GetConnection(){ 
        try{
            using MySqlConnection connection = new MySqlConnection(Environment.GetEnvironmentVariable("MYSQL_DIALECTOR"));
            return (connection,null);
        }   catch(Exception err){
            return (null,err);
        }
    }
}