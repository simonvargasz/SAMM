using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SAMM.Decoder.Infraestructure
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ExecuteStoredProcedure(string procedureName, MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
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

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
                }
            }
        }

        public DataTable ExecuteSelectQuery(string query, MySqlParameter[] parameters = null)
        {
            DataTable resultTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(resultTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al ejecutar la consulta: {ex.Message}");
                }
            }

            return resultTable;
        }

        public int GetLastInsertedId()
        {
            int lastId = 0;
            string query = "SELECT LAST_INSERT_ID()";

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    lastId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return lastId;
        }
    }
}
