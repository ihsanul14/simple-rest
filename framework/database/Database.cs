using MySql.Data.MySqlClient;
using simple_rest.framework.config;

namespace simple_rest.framework.database;

public class Database{
    public (MySqlConnection?,Exception?) GetConnection(){ 
        try{
            using MySqlConnection connection = new MySqlConnection(Config.DefaultConfig?.GetValue<string>("Database:Mysql_Dialector"));
            return (connection,null);
        }   catch(Exception err){
            return (null,err);
        }
    }
}