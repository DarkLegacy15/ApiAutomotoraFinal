using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiAutomotoraFinal.Models;


namespace ApiAutomotoraFinal.Azure
{
    public class DetallesAzure
    {


        static string connectionString = @"Server=(localdb)\ELERIOS;Database=REK;Trusted_Connection=True;";

        private static List<Detalle> vehiculos;


        //Abrir Conexiones
        private static SqlCommand AbrirConexionSqlDetalles(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "SELECT * FROM Detalle";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static SqlCommand AbrirConexionSqlDetalle(SqlConnection sqlConnection, string query)
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
        private static List<Detalle> ListarDetalle(DataTable dataTable)
        {
            vehiculos = new List<Detalle>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Detalle vehiculo = new Detalle();
                vehiculo.IdDetalle = int.Parse(dataTable.Rows[i]["idDetalle"].ToString());
                vehiculo.IdCompra = int.Parse(dataTable.Rows[i]["idCompra"].ToString());
                vehiculo.IdVehiculo = int.Parse(dataTable.Rows[i]["idVehiculos"].ToString());
                
                vehiculos.Add(vehiculo);
            }

            return vehiculos;
        }
        private static Detalle CreacionDetalle(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Detalle vehiculo = new Detalle();
                vehiculo.IdDetalle = int.Parse(dataTable.Rows[0]["idDetalle"].ToString());
                vehiculo.IdCompra = int.Parse(dataTable.Rows[0]["idCompra"].ToString());
                vehiculo.IdVehiculo = int.Parse(dataTable.Rows[0]["idVehiculos"].ToString());
                return vehiculo;
            }
            else
            {
                return null;
            }
        }

        public static List<Detalle> ObtenerDetalle()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlDetalles(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarDetalle(dataTable);
            }
        }
      
        public static Detalle ObtenerDetalle(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Detalle WHERE idDetalle = '{id}'";

                var comando = AbrirConexionSqlDetalle(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionDetalle(dataTable);

            }
        }
        public static int AgregarDetalle(Detalle vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "INSERT INTO Detalle (idCompra,idVehiculos) values (@compra,@auto)";

                sqlCommand.Parameters.AddWithValue("@compra", vehiculo.IdCompra);
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

        public static int EliminarDetallePorID(string id)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "DELETE FROM Detalle WHERE idDetalle = @id";

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

        public static int ActualizarDetallePorId(Detalle vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "UPDATE Detalle SET idCompra = @compra, idVehiculo = @auto WHERE idDetalle = @idDetalle";

                sqlCommand.Parameters.AddWithValue("@compra", vehiculo.IdCompra);
                sqlCommand.Parameters.AddWithValue("@auto", vehiculo.IdVehiculo);
                sqlCommand.Parameters.AddWithValue("@idDetalle", vehiculo.IdDetalle);
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
