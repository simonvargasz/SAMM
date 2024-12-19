using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SAMM.DataAccess.Infrastructure
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DataTable ExecuteStoredProcedure(string procedureName, MySqlParameter[] parameters)
        {
            DataTable resultTable = new();

            using (MySqlConnection connection = new(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (MySqlDataAdapter adapter = new(command))
                        {
                            adapter.Fill(resultTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
                }
            }

            return resultTable;
        }
    }
}