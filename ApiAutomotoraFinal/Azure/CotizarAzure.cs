using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiAutomotoraFinal.Models;

namespace ApiAutomotoraFinal.Azure
{
    public class CotizarAzure
    {


        static string connectionString = @"Server=(localdb)\ELERIOS;Database=REK;Trusted_Connection=True;";

        private static List<Cotizar> vehiculos;


        //Abrir Conexiones
        private static SqlCommand AbrirConexionSqlCotizar2(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "SELECT * FROM Cotizar";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static SqlCommand AbrirConexionSqlCotizar(SqlConnection sqlConnection, string query)
        {
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlConnection.Open();
            return sqlCommand;
        }

        private static DataTable LLenadoTabla(SqlCommand comando)
        {
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter(comando);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }
        private static List<Cotizar> ListarCotizar(DataTable dataTable)
        {
            vehiculos = new List<Cotizar>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Cotizar vehiculo = new Cotizar();
                vehiculo.IdCotizar = int.Parse(dataTable.Rows[i]["IdCotizar"].ToString());
                vehiculo.RutUsuario = dataTable.Rows[i]["RutUsuario"].ToString();
                vehiculo.IdVehiculo = int.Parse(dataTable.Rows[i]["IdVehiculo"].ToString());
                vehiculos.Add(vehiculo);
            }

            return vehiculos;
        }
        private static Cotizar CreacionCotizar(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Cotizar vehiculo = new Cotizar();
                vehiculo.IdCotizar = int.Parse(dataTable.Rows[0]["IdCotizar"].ToString());
                vehiculo.RutUsuario = dataTable.Rows[0]["RutUsuario"].ToString();
                vehiculo.IdVehiculo = int.Parse(dataTable.Rows[0]["IdVehiculo"].ToString());
                return vehiculo;
            }
            else
            {
                return null;
            }
        }

        public static List<Cotizar> ObtenerCotizar()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlCotizar2(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarCotizar(dataTable);
            }
        }
        
        public static Cotizar ObtenerCotizar(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Cotizar WHERE IdCotizar = '{id}'";

                var comando = AbrirConexionSqlCotizar(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionCotizar(dataTable);

            }
        }
        public static int AgregarCotizar(Cotizar vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "INSERT INTO Cotizar (RutUsuario,IdVehiculo) values (@rut,@auto)";

                sqlCommand.Parameters.AddWithValue("@rut", vehiculo.RutUsuario);
                sqlCommand.Parameters.AddWithValue("@auto", vehiculo.IdVehiculo);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }
        }

        public static int EliminarCotizarPorID(string id)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "DELETE FROM Cotizar WHERE IdCotizar = @id";

                sqlCommand.Parameters.AddWithValue("@id", id);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }
        }

        public static int ActualizarCotizarPorId(Cotizar vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "UPDATE Cotizar SET RutUsuario = @rut, IdVehiculo = @auto WHERE IdCotizar = @IdCotizar";

                sqlCommand.Parameters.AddWithValue("@rut", vehiculo.RutUsuario);
                sqlCommand.Parameters.AddWithValue("@auto", vehiculo.IdVehiculo);
                sqlCommand.Parameters.AddWithValue("@IdCotizar", vehiculo.IdCotizar);

                try
                {
                    sqlConnection.Open();
                    filasAfectadas = sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return filasAfectadas;
            }
        }








    }
}
