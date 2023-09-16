//Librerias del ADO .NET
using System.Data.SqlClient;
using System.Data;
using ConsoleApp1;
public class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-10\\SQLEXPRESS;User Id=user_tecsup;Password=123456";


    static void Main()
    {
        // Llamar a la función para obtener los datos como DataTable
        DataTable dataTable = ListarEmpleadosDataTable();

        // Mostrar los datos del DataTable (esto es solo un ejemplo)
        foreach (DataRow row in dataTable.Rows)
        {
            Console.WriteLine($"ID: {row["idTrabajador"]}, Nombre: {row["nombre"]}, Apellido: {row["apellido"]}, Sueldo: {row["sueldo"]}, Fecha de Nacimiento: {row["fechaNacimiento"]}");
        }

        // Llamar a la función para obtener los datos como una lista de objetos
        List<Trabajador> trabajadores = ListarEmpleadosListaObjetos();

        // Mostrar los datos de la lista de objetos (esto es solo un ejemplo)
        foreach (Trabajador trabajador in trabajadores)
        {
            Console.WriteLine($"ID: {trabajador.TrabajadorId}, Nombre: {trabajador.Nombre}, Apellido: {trabajador.Apellido}, Sueldo: {trabajador.Sueldo}, Fecha de Nacimiento: {trabajador.FechaNacimiento}");
        }

        Console.ReadLine();
    }


    //De forma desconectada
    private static DataTable ListarEmpleadosDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Trabajadores";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);


            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }

    //De forma conectada
    public static List<Trabajador> ListarEmpleadosListaObjetos()
    {
        List<Trabajador> trabajadores = new List<Trabajador>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT idTrabajador,nombre,apellido,sueldo,fechaNacimiento FROM Trabajadores";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila
                            trabajadores.Add(new Trabajador
                            {
                                TrabajadorId = (int)reader["idTrabajador"],
                                Nombre = reader["nombre"].ToString(),
                                Apellido = reader["apellido"].ToString(),
                                Sueldo = reader["sueldo"] == DBNull.Value ? 0 : (decimal)reader["sueldo"],
                                FechaNacimiento = (DateTime)reader["fechaNacimiento"]
                            });

                        }

                    }
                }
            }

            // Cerrar la conexión
            connection.Close();


        }
        return trabajadores;

    }


}