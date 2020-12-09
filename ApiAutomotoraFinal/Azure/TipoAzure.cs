using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiAutomotoraFinal.Models;

namespace ApiAutomotoraFinal.Azure
{
    public class TipoAzure
    {



        static string connectionString = @"Server=(localdb)\ELERIOS;Database=REK;Trusted_Connection=True;";

        private static List<Tipo> vehiculos;


        //Abrir Conexiones
        private static SqlCommand AbrirConexionSqlTipos(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "SELECT * FROM Tipo";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static SqlCommand AbrirConexionSqlTipo(SqlConnection sqlConnection, string query)
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
        private static List<Tipo> ListarTipo(DataTable dataTable)
        {
            vehiculos = new List<Tipo>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Tipo tipo = new Tipo();
                tipo.IdTipo = int.Parse(dataTable.Rows[i]["idTipo"].ToString());
                tipo.Vehiculo = dataTable.Rows[i]["Vehiculo"].ToString();
                vehiculos.Add(tipo);
            }

            return vehiculos;
        }
        private static Tipo CreacionTipo(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Tipo tipo = new Tipo();
                tipo.IdTipo = int.Parse(dataTable.Rows[0]["idTipo"].ToString());
                tipo.Vehiculo = dataTable.Rows[0]["Vehiculo"].ToString();
                return tipo;
            }
            else
            {
                return null;
            }
        }

        public static List<Tipo> ObtenerTipo()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlTipos(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarTipo(dataTable);
            }
        }
        public static Tipo ObtenerTipo(string vehiculo)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Tipo WHERE Vehiculo = '{vehiculo}'";

                var comando = AbrirConexionSqlTipo(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionTipo(dataTable);

            }
        }
        public static Tipo ObtenerTipo(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Tipo WHERE idTipo = '{id}'";

                var comando = AbrirConexionSqlTipo(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionTipo(dataTable);

            }
        }
        public static int AgregarTipo(Tipo vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "INSERT INTO Tipo (Vehiculo) values (@nombre)";

                
                sqlCommand.Parameters.AddWithValue("@nombre", vehiculo.Vehiculo);

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

        public static int EliminarTipoPorID(string id)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "DELETE FROM Tipo WHERE idTipo = @id";

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

        public static int ActualizarTipoPorId(Tipo vehiculo)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "UPDATE Tipo SET Tipo = @nombre WHERE idTipo = @idTipo";


                sqlCommand.Parameters.AddWithValue("@nombre", vehiculo.Vehiculo);
                sqlCommand.Parameters.AddWithValue("@idTipo", vehiculo.IdTipo);
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
