using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiAutomotoraFinal.Models;


namespace ApiAutomotoraFinal.Azure
{
    public class UsuarioAzure
    {


        static string connectionString = @"Server=(localdb)\ELERIOS;Database=REK;Trusted_Connection=True;";

        private static List<Usuario> vehiculos;


        //Abrir Conexiones
        private static SqlCommand AbrirConexionSqlUsuarios(SqlConnection sqlConnection)
        {
            SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
            sqlCommand.CommandText = "SELECT * FROM Usuario";
            sqlConnection.Open();
            return sqlCommand;
        }
        private static SqlCommand AbrirConexionSqlUsuario(SqlConnection sqlConnection, string query)
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
        private static List<Usuario> ListarUsuario(DataTable dataTable)
        {
            vehiculos = new List<Usuario>();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                Usuario usuarios = new Usuario();
                usuarios.RutUsuario = dataTable.Rows[i]["RutUsuario"].ToString();
                usuarios.Nombre = dataTable.Rows[i]["Nombre"].ToString();
                usuarios.Apellido =dataTable.Rows[i]["Apellido"].ToString();
                usuarios.Edad = int.Parse(dataTable.Rows[i]["Edad"].ToString());
                usuarios.Email = dataTable.Rows[i]["Email"].ToString();
                vehiculos.Add(usuarios);
            }

            return vehiculos;
        }
        private static Usuario CreacionUsuario(DataTable dataTable)
        {
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                Usuario usuarios = new Usuario();
                usuarios.RutUsuario = dataTable.Rows[0]["RutUsuario"].ToString();
                usuarios.Nombre = dataTable.Rows[0]["Nombre"].ToString();
                usuarios.Apellido = dataTable.Rows[0]["Apellido"].ToString();
                usuarios.Edad = int.Parse(dataTable.Rows[0]["Edad"].ToString());
                usuarios.Email = dataTable.Rows[0]["Email"].ToString();
                return usuarios;
            }
            else
            {
                return null;
            }
        }

        public static List<Usuario> ObtenerUsuarios()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var comando = AbrirConexionSqlUsuarios(sqlConnection);

                var dataTable = LLenadoTabla(comando);

                return ListarUsuario(dataTable);
            }
        }
        public static Usuario ObtenerUsuario(string rut)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var query = $"SELECT * FROM Usuario WHERE RutUsuario = '{rut}'";

                var comando = AbrirConexionSqlUsuario(sqlConnection, query);

                var dataTable = LLenadoTabla(comando);

                return CreacionUsuario(dataTable);

            }
        }
        
        public static int AgregarUsuario(Usuario usuarios)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "INSERT INTO Usuario (RutUsuario,Nombre,Apellido,Edad,Email) values (@rut,@nombre,@apellido,@edad,@email)";

                sqlCommand.Parameters.AddWithValue("@rut", usuarios.RutUsuario);
                sqlCommand.Parameters.AddWithValue("@nombre", usuarios.Nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", usuarios.Apellido);
                sqlCommand.Parameters.AddWithValue("@edad", usuarios.Edad);
                sqlCommand.Parameters.AddWithValue("@email", usuarios.Email);

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

        public static int EliminarUsuarioPorRut(string rut)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "DELETE FROM Usuario WHERE RutUsuario = @rut";

                sqlCommand.Parameters.AddWithValue("@rut", rut);

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

        public static int ActualizarUsuarioPorRut(Usuario usuarios)
        {
            int filasAfectadas = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand(null, sqlConnection);
                sqlCommand.CommandText = "UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, Edad = @edad, Email = @email WHERE RutUsuario = @RutUsuario";

                sqlCommand.Parameters.AddWithValue("@nombre", usuarios.Nombre);
                sqlCommand.Parameters.AddWithValue("@apellido", usuarios.Apellido);
                sqlCommand.Parameters.AddWithValue("@edad", usuarios.Edad);
                sqlCommand.Parameters.AddWithValue("@email", usuarios.Email);
                sqlCommand.Parameters.AddWithValue("@RutUsuario", usuarios.RutUsuario);
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
