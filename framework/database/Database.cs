using simple_rest.framework.config;
using simple_rest.domain.models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;

namespace simple_rest.framework.database;

public class Database : DbContext{
    public (IDbConnection?,Exception?) GetConnection(){ 
        try{
            IDbConnection dbConnection = new MySqlConnection(Config.DefaultConfig?.GetValue<string>("Database:Mysql_Dialector"));
            return (dbConnection, null);
        }   catch(Exception err){
            return (null,err);
        }
    }
}