using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiAutomotoraFinal.Models;

namespace ApiAutomotoraFinal.Azure
{
    public class CompraAzure
    {


        static string connectionString = @"Server=(localdb)\ELERIOS;Database=REK;Trusted_Connection=True;";

        private static List<Compra> vehiculos;


        //Abrir Conexiones
        private static SqlCommand AbrirConexionSqlCompras(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "SELECT * FROM Compra";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static SqlCommand AbrirConexionSqlCompra(SqlConnection sqlConnection, string query)
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
        private static List<Compra> ListarCompra(DataTable dataTable)
        {
            vehiculos = new List<Compra>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Compra vehiculo = new Compra();
                vehiculo.IdCompra = int.Parse(dataTable.Rows[i]["idCompra"].ToString());
                vehiculo.Fecha = DateTime.Parse(dataTable.Rows[i]["Fecha"].ToString());
                vehiculo.Total = int.Parse(dataTable.Rows[i]["Total"].ToString());
                vehiculo.RutUsuario = dataTable.Rows[i]["RutUsuario"].ToString();
                vehiculos.Add(vehiculo);
            }

            return vehiculos;
        }
        private static Compra CreacionCompra(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Compra vehiculo = new Compra();
                vehiculo.IdCompra = int.Parse(dataTable.Rows[0]["idCompra"].ToString());
                vehiculo.Fecha = DateTime.Parse(dataTable.Rows[0]["Fecha"].ToString());
                vehiculo.Total = int.Parse(dataTable.Rows[0]["Total"].ToString());
                vehiculo.RutUsuario = dataTable.Rows[0]["RutUsuario"].ToString();
                return vehiculo;
            }
            else
            {
                return null;
            }
        }

        public static List<Compra> ObtenerCompra()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlCompras(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarCompra(dataTable);
            }
        }
        
        public static Compra ObtenerCompra(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Compra WHERE idCompra = '{id}'";

                var comando = AbrirConexionSqlCompra(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionCompra(dataTable);

            }
        }
        public static int AgregarCompra(Compra vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "INSERT INTO Compra (Fecha,Total,RutUsuario) values (@fecha,@total,@rut)";

                sqlCommand.Parameters.AddWithValue("@fecha", vehiculo.Fecha);
                sqlCommand.Parameters.AddWithValue("@total", vehiculo.Total);
                sqlCommand.Parameters.AddWithValue("@rut", vehiculo.RutUsuario);

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

        public static int EliminarCompraPorID(string id)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "DELETE FROM Compra WHERE IdCompra = @id";

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

        public static int ActualizarCompraPorId(Compra vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "UPDATE Compra SET Fecha = @fecha, Total = @total, RutUsuario = @rut WHERE idCompra = @idCompra";

                sqlCommand.Parameters.AddWithValue("@fecha", vehiculo.Fecha);
                sqlCommand.Parameters.AddWithValue("@total", vehiculo.Total);
                sqlCommand.Parameters.AddWithValue("@rut", vehiculo.RutUsuario);
                sqlCommand.Parameters.AddWithValue("@idCompra", vehiculo.IdCompra);

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
