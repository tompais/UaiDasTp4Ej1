using ABS;
using CONTEXT;
using DOM;
using Microsoft.Data.SqlClient;
using System.Data;

namespace REPO
{
    public class AlumnoRepository(DatabaseContext context) : IAlumnoRepository
    {
        public IEnumerable<Alumno> GetAll()
        {
        var alumnos = new List<Alumno>();

            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Alumno_GetAll", connection)
            {
       CommandType = CommandType.StoredProcedure
            };

   connection.Open();
     using var reader = command.ExecuteReader();

   while (reader.Read())
            {
        alumnos.Add(MapFromReader(reader));
  }

          return alumnos;
        }

        public Alumno? GetById(int id)
 {
         using var connection = (SqlConnection)context.CreateConnection();
 using var command = new SqlCommand("sp_Alumno_GetById", connection)
    {
       CommandType = CommandType.StoredProcedure
       };

        command.Parameters.AddWithValue("@Id", id);

            connection.Open();
   using var reader = command.ExecuteReader();

            if (reader.Read())
            {
             return MapFromReader(reader);
     }

    return null;
        }

        public Alumno? GetByDni(string dni)
  {
     using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Alumno_GetByDni", connection)
            {
             CommandType = CommandType.StoredProcedure
   };

  command.Parameters.AddWithValue("@Dni", dni);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapFromReader(reader);
  }

       return null;
        }

        public int Add(Alumno entity)
        {
        using var connection = (SqlConnection)context.CreateConnection();
    using var command = new SqlCommand("sp_Alumno_Insert", connection)
            {
      CommandType = CommandType.StoredProcedure
       };

 command.Parameters.AddWithValue("@Nombre", entity.Nombre);
    command.Parameters.AddWithValue("@Apellido", entity.Apellido);
            command.Parameters.AddWithValue("@Dni", entity.Dni);
      command.Parameters.AddWithValue("@Email", entity.Email);
        command.Parameters.AddWithValue("@Telefono", entity.Telefono);
     command.Parameters.AddWithValue("@FechaNacimiento", entity.FechaNacimiento);

        var outputParam = new SqlParameter("@Id", SqlDbType.Int)
            {
Direction = ParameterDirection.Output
  };
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();

   return (int)outputParam.Value;
        }

        public bool Update(Alumno entity)
        {
  using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Alumno_Update", connection)
            {
     CommandType = CommandType.StoredProcedure
       };

     command.Parameters.AddWithValue("@Id", entity.Id);
   command.Parameters.AddWithValue("@Nombre", entity.Nombre);
       command.Parameters.AddWithValue("@Apellido", entity.Apellido);
      command.Parameters.AddWithValue("@Dni", entity.Dni);
            command.Parameters.AddWithValue("@Email", entity.Email);
            command.Parameters.AddWithValue("@Telefono", entity.Telefono);
            command.Parameters.AddWithValue("@FechaNacimiento", entity.FechaNacimiento);
   command.Parameters.AddWithValue("@Activo", entity.Activo);

         connection.Open();
     var result = command.ExecuteNonQuery();

            return result > 0;
        }

        public bool Delete(int id)
        {
using var connection = (SqlConnection)context.CreateConnection();
       using var command = new SqlCommand("sp_Alumno_Delete", connection)
         {
        CommandType = CommandType.StoredProcedure
   };

            command.Parameters.AddWithValue("@Id", id);

      connection.Open();
     var result = command.ExecuteNonQuery();

            return result > 0;
        }

public Alumno MapFromReader(IDataRecord reader) => new()
     {
         Id = reader.GetInt32(reader.GetOrdinal("Id")),
  Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
            Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
            Dni = reader.GetString(reader.GetOrdinal("Dni")),
            Email = reader.GetString(reader.GetOrdinal("Email")),
      Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
            FechaNacimiento = reader.GetDateTime(reader.GetOrdinal("FechaNacimiento")),
      Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
        };
    }
}
