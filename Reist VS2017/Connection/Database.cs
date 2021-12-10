using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Reist_VS2017.Connection
{
    public class Database : IDisposable
    {
        public MySqlConnection connection;
        public MySqlCommand command;

        public Database()
        {
            connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        public void ExecuteCommand(string comando)
        {
            command = new MySqlCommand(comando, connection);
            command.ExecuteNonQuery();
        }

        public MySqlDataReader ReturnCommand(string comando)
        {
            command = new MySqlCommand(comando, connection);
            return command.ExecuteReader();
        }

        public void Dispose()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }
}