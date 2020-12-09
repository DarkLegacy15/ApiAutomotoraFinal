using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiAutomotoraFinal.Models;


namespace ApiAutomotoraFinal.Azure
{
    public class MarcaAzure
    {



        static string connectionString = @"Server=(localdb)\ELERIOS;Database=REK;Trusted_Connection=True;";

        private static List<Marca> vehiculos;


        //Abrir Conexiones
        private static SqlCommand AbrirConexionSqlMarca(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "SELECT * FROM Marca";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static SqlCommand AbrirConexionSqlMarca(SqlConnection sqlConnection, string query)
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
        private static List<Marca> ListarMarca(DataTable dataTable)
        {
            vehiculos = new List<Marca>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Marca vehiculo = new Marca();
                vehiculo.IdMarca = int.Parse(dataTable.Rows[i]["idMarca"].ToString());
                vehiculo.Nombre = dataTable.Rows[i]["Nombre"].ToString();
                vehiculo.Pais = dataTable.Rows[i]["Pais"].ToString();
                vehiculos.Add(vehiculo);
            }

            return vehiculos;
        }
        private static Marca CreacionMarca(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Marca vehiculo = new Marca();
                vehiculo.IdMarca = int.Parse(dataTable.Rows[0]["idMarca"].ToString());
                vehiculo.Nombre = dataTable.Rows[0]["Nombre"].ToString();
                vehiculo.Pais = dataTable.Rows[0]["Pais"].ToString();
                return vehiculo;
            }
            else
            {
                return null;
            }
        }

        public static List<Marca> ObtenerMarca()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlMarca(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarMarca(dataTable);
            }
        }
        public static Marca ObtenerMarca(string pais)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Marca WHERE Pais = '{pais}'";

                var comando = AbrirConexionSqlMarca(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionMarca(dataTable);

            }
        }
        public static Marca ObtenerMarca(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Marca WHERE idMarca = '{id}'";

                var comando = AbrirConexionSqlMarca(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionMarca(dataTable);

            }
        }
        public static int AgregarMarca(Marca vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "INSERT INTO Marca (Nombre,Pais) values (@nombre,@pais)";

                sqlCommand.Parameters.AddWithValue("@nombre", vehiculo.Nombre);
                sqlCommand.Parameters.AddWithValue("@pais", vehiculo.Pais);

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

        public static int EliminarMarcaPorID(string id)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "DELETE FROM Marca WHERE idMarca = @id";

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

        public static int ActualizarMarcaPorId(Marca vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "UPDATE Marca SET Nombre = @nombre, Pais = @pais WHERE idMarca = @idMarca";

                sqlCommand.Parameters.AddWithValue("@nombre", vehiculo.Nombre);
                sqlCommand.Parameters.AddWithValue("@pais", vehiculo.Pais);
                sqlCommand.Parameters.AddWithValue("@idMarca", vehiculo.IdMarca);
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
