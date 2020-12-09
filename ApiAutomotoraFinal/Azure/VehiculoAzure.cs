using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiAutomotoraFinal.Models;



namespace ApiAutomotoraFinal.Azure
{
    public class VehiculoAzure
    {



        static string connectionString = @"Server=(localdb)\ELERIOS;Database=REK;Trusted_Connection=True;";

        private static List<Vehiculo> vehiculos;


        //Abrir Conexiones
        private static SqlCommand AbrirConexionSqlVehiculos(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "SELECT * FROM Vehiculo";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static SqlCommand AbrirConexionSqlVehiculos(SqlConnection sqlConnection, string query)
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
        private static List<Vehiculo> ListarVehiculos(DataTable dataTable)
        {
            vehiculos = new List<Vehiculo>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Vehiculo vehiculo = new Vehiculo();
                vehiculo.IdVehiculo = int.Parse(dataTable.Rows[i]["idVehiculo"].ToString());
                vehiculo.Modelo = dataTable.Rows[i]["Modelo"].ToString();
                vehiculo.IdMarca = int.Parse(dataTable.Rows[i]["idMarca"].ToString());
                vehiculo.Estilo = dataTable.Rows[i]["Estilo"].ToString();
                vehiculo.IdTipo = int.Parse(dataTable.Rows[i]["idTipo"].ToString());
                vehiculos.Add(vehiculo);
            }

            return vehiculos;
        }
        private static Vehiculo CreacionVehiculo(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Vehiculo vehiculo = new Vehiculo();
                vehiculo.IdVehiculo = int.Parse(dataTable.Rows[0]["idVehiculo"].ToString());
                vehiculo.Modelo = dataTable.Rows[0]["Modelo"].ToString();
                vehiculo.IdMarca = int.Parse(dataTable.Rows[0]["idMarca"].ToString());
                vehiculo.Estilo = dataTable.Rows[0]["Estilo"].ToString();
                vehiculo.IdTipo = int.Parse(dataTable.Rows[0]["idTipo"].ToString());
                return vehiculo;
            }
            else
            {
                return null;
            }
        }

        public static List<Vehiculo> ObtenerVehiculos()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlVehiculos(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarVehiculos(dataTable);
            }
        }
        public static Vehiculo ObtenerVehiculos(string modelo)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Vehiculo WHERE Modelo = '{modelo}'";

                var comando = AbrirConexionSqlVehiculos(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionVehiculo(dataTable);

            }
        }
        public static Vehiculo ObtenerVehiculo(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Vehiculo WHERE idVehiculo = '{id}'";

                var comando = AbrirConexionSqlVehiculos(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionVehiculo(dataTable);

            }
        }
        public static int AgregarVehiculo(Vehiculo vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "INSERT INTO Vehiculo (Modelo,idMarca,Estilo,idTipo) values (@nombre,@marca,@estilo,@tipo)";

                sqlCommand.Parameters.AddWithValue("@nombre", vehiculo.Modelo);
                sqlCommand.Parameters.AddWithValue("@marca", vehiculo.IdMarca);
                sqlCommand.Parameters.AddWithValue("@estilo", vehiculo.Estilo);
                sqlCommand.Parameters.AddWithValue("@tipo", vehiculo.IdTipo);

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
        
        public static int EliminarVehiculoPorID(string id)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "DELETE FROM Vehiculo WHERE idVehiculo = @id";

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

        public static int ActualizarVehiculoPorId(Vehiculo vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "UPDATE Vehiculo SET Modelo = @modelo, idMarca = @marca, Estilo = @estilo, idTipo = @tipo WHERE idVehiculo = @idVehiculo";

                sqlCommand.Parameters.AddWithValue("@modelo", vehiculo.Modelo);
                sqlCommand.Parameters.AddWithValue("@marca", vehiculo.IdMarca);
                sqlCommand.Parameters.AddWithValue("@estilo", vehiculo.Estilo);
                sqlCommand.Parameters.AddWithValue("@tipo", vehiculo.IdTipo);
                sqlCommand.Parameters.AddWithValue("@idVehiculo", vehiculo.IdVehiculo);
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
