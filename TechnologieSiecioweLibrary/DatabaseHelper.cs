using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnologieSiecioweLibrary.Interfaces;

namespace TechnologieSiecioweLibrary
{
    public class DatabaseHelper
    {

        #region Proceduralne (Prawdopodobnie zostają)
        public static async Task<DataTable> ExecuteStoredProcedureWithResult(string storedProcedureName, Dictionary<string, object> parameters, IConfig config)
        {
            DataTable resultTable = new DataTable();
            string connectionString = GetConnectionString(config);
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            resultTable.Load(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

            }
            catch (Exception ex)
            {

            }

            return resultTable;
        }



        public static async Task<int> ExecuteStoredProcedure(string storedProcedureName, Dictionary<string, object> parameters, IConfig config)
        {
            string connectionString = GetConnectionString(config);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }

                        int affectedRows = await command.ExecuteNonQueryAsync();
                        return affectedRows;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                return -1;
            }
        }

        #endregion




        public static string GetConnectionString(IConfig config)
        {
            string connectionString;
            string serverName = "";
            string databaseName = "";
            string userName = "";
            string userPassword = "";
            bool useIntegratedSecurity = false;



            serverName = config.SQL_ServerName;
            databaseName = config.SQL_DatabaseName;
            useIntegratedSecurity = config.SQL_UseIntegratedSecurity;
            userName = config.SQL_Login;
            userPassword = Kodek.Decrypt(config.SQL_EncryptedPassword);



            if (useIntegratedSecurity)
            {
                connectionString = $"Server={serverName}; Database={databaseName}; Integrated Security=True;";
            }
            else
            {
                connectionString = $"Server={serverName}; User Id={userName}; Password={userPassword}; Database={databaseName};";
            }
            return connectionString;
        }

        public static async Task<bool> CheckConnection(IConfig config)
        {
            string connectionString;
            bool result = false;


            if (config.SQL_UseIntegratedSecurity)
            {
                connectionString = $"Server={config.SQL_ServerName}; Database={config.SQL_DatabaseName}; Integrated Security=True;";
            }
            else
            {
                connectionString = $"Server={config.SQL_ServerName}; Database={config.SQL_DatabaseName}; User Id={config.SQL_Login}; Password={Kodek.Decrypt(config.SQL_EncryptedPassword)};";
            }



            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    result = true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        public static async Task<DataTable> ExecuteQueryAsync(string query, IConfig config)
        {
            DataTable resultTable = new DataTable();

            string connectionString = GetConnectionString(config);

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            resultTable.Load(reader);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }

            return resultTable;
        }


    }
}
