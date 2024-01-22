using simple_rest.framework.config;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;
using NHibernate.Driver;

namespace simple_rest.framework.database;

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