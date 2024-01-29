using rest.Framework.ConfigFramework;
using MySql.Data.MySqlClient;
using System.Data;

namespace rest.Framework.DatabaseFramework;

public interface IDatabase{
    IDbConnection? ConnectMySQL();
}

public class Database : IDatabase{

    public IDbConnection? MySQL;
    public Database (){ 
        MySQL = ConnectMySQL();
    }

    public IDbConnection? ConnectMySQL(){
        IDbConnection res = new MySqlConnection(Config.DefaultConfig?.GetValue<string>("Database:MYSQL_DIALECTOR"));
        return res;
    }
}