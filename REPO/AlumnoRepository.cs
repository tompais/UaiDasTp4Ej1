using System.Data;
using ABS;
using CONTEXT;
using DOM;
using Microsoft.Data.SqlClient;

namespace REPO
{
    public class AlumnoRepository : IAlumnoRepository
    {
        private readonly DatabaseContext _context;

        public AlumnoRepository(DatabaseContext context)
      {
          _context = context ?? throw new ArgumentNullException(nameof(context));
 }

      public async Task<IEnumerable<Alumno>> GetAllAsync()
        {
            var alumnos = new List<Alumno>();
            
            using var connection = (SqlConnection)_context.CreateConnection();
            using var command = new SqlCommand("sp_Alumno_GetAll", connection)
      {
  CommandType = CommandType.StoredProcedure
     };

          await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
     
            while (await reader.ReadAsync())
 {
             alumnos.Add(MapFromReader(reader));
       }

          return alumnos;
        }

        public async Task<Alumno?> GetByIdAsync(int id)
        {
    using var connection = (SqlConnection)_context.CreateConnection();
   using var command = new SqlCommand("sp_Alumno_GetById", connection)
 {
  CommandType = CommandType.StoredProcedure
       };

          command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
       {
        return MapFromReader(reader);
       }

      return null;
        }

      public async Task<Alumno?> GetByDniAsync(string dni)
        {
            using var connection = (SqlConnection)_context.CreateConnection();
   using var command = new SqlCommand("sp_Alumno_GetByDni", connection)
     {
          CommandType = CommandType.StoredProcedure
       };

     command.Parameters.AddWithValue("@Dni", dni);

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

       if (await reader.ReadAsync())
     {
       return MapFromReader(reader);
            }

            return null;
   }

        public async Task<int> AddAsync(Alumno entity)
        {
      using var connection = (SqlConnection)_context.CreateConnection();
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

            await connection.OpenAsync();
          await command.ExecuteNonQueryAsync();

      return (int)outputParam.Value;
        }

        public async Task<bool> UpdateAsync(Alumno entity)
        {
      using var connection = (SqlConnection)_context.CreateConnection();
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

 await connection.OpenAsync();
          var result = await command.ExecuteNonQueryAsync();

    return result > 0;
        }

   public async Task<bool> DeleteAsync(int id)
      {
    using var connection = (SqlConnection)_context.CreateConnection();
            using var command = new SqlCommand("sp_Alumno_Delete", connection)
       {
            CommandType = CommandType.StoredProcedure
            };

      command.Parameters.AddWithValue("@Id", id);

            await connection.OpenAsync();
      var result = await command.ExecuteNonQueryAsync();

            return result > 0;
    }

     private static Alumno MapFromReader(IDataRecord reader)
 {
         return new Alumno
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
}
