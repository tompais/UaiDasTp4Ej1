using System.Data;
using ABS;
using CONTEXT;
using DOM;
using Microsoft.Data.SqlClient;

namespace REPO
{
    public class PrestamoRepository : IPrestamoRepository
    {
   private readonly DatabaseContext _context;

 public PrestamoRepository(DatabaseContext context)
        {
    _context = context ?? throw new ArgumentNullException(nameof(context));
     }

    public async Task<IEnumerable<Prestamo>> GetAllAsync()
   {
     var prestamos = new List<Prestamo>();
            
 using var connection = (SqlConnection)_context.CreateConnection();
     using var command = new SqlCommand("sp_Prestamo_GetAll", connection)
       {
 CommandType = CommandType.StoredProcedure
     };

       await connection.OpenAsync();
    using var reader = await command.ExecuteReaderAsync();
       
      while (await reader.ReadAsync())
      {
       prestamos.Add(MapFromReader(reader));
    }

     return prestamos;
     }

        public async Task<Prestamo?> GetByIdAsync(int id)
        {
    using var connection = (SqlConnection)_context.CreateConnection();
     using var command = new SqlCommand("sp_Prestamo_GetById", connection)
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

        public async Task<IEnumerable<Prestamo>> GetByAlumnoIdAsync(int alumnoId)
        {
        var prestamos = new List<Prestamo>();
      
   using var connection = (SqlConnection)_context.CreateConnection();
     using var command = new SqlCommand("sp_Prestamo_GetByAlumnoId", connection)
            {
      CommandType = CommandType.StoredProcedure
  };

     command.Parameters.AddWithValue("@AlumnoId", alumnoId);

   await connection.OpenAsync();
      using var reader = await command.ExecuteReaderAsync();
       
 while (await reader.ReadAsync())
            {
       prestamos.Add(MapFromReader(reader));
          }

            return prestamos;
        }

        public async Task<IEnumerable<Prestamo>> GetPrestamosActivosAsync()
      {
   var prestamos = new List<Prestamo>();
     
  using var connection = (SqlConnection)_context.CreateConnection();
      using var command = new SqlCommand("sp_Prestamo_GetActivos", connection)
     {
    CommandType = CommandType.StoredProcedure
   };

   await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();
     
       while (await reader.ReadAsync())
        {
       prestamos.Add(MapFromReader(reader));
     }

  return prestamos;
   }

        public async Task<IEnumerable<Prestamo>> GetPrestamosVencidosAsync()
        {
  var prestamos = new List<Prestamo>();
            
       using var connection = (SqlConnection)_context.CreateConnection();
       using var command = new SqlCommand("sp_Prestamo_GetVencidos", connection)
          {
       CommandType = CommandType.StoredProcedure
     };

       await connection.OpenAsync();
     using var reader = await command.ExecuteReaderAsync();
         
  while (await reader.ReadAsync())
      {
         prestamos.Add(MapFromReader(reader));
            }

   return prestamos;
        }

        public async Task<int> AddAsync(Prestamo entity)
        {
       using var connection = (SqlConnection)_context.CreateConnection();
            using var command = new SqlCommand("sp_Prestamo_Insert", connection)
       {
        CommandType = CommandType.StoredProcedure
    };

   command.Parameters.AddWithValue("@AlumnoId", entity.AlumnoId);
       command.Parameters.AddWithValue("@EjemplarId", entity.EjemplarId);
 command.Parameters.AddWithValue("@FechaPrestamo", entity.FechaPrestamo);
       command.Parameters.AddWithValue("@FechaDevolucionPrevista", entity.FechaDevolucionPrevista);

            var outputParam = new SqlParameter("@Id", SqlDbType.Int)
  {
           Direction = ParameterDirection.Output
      };
      command.Parameters.Add(outputParam);

        await connection.OpenAsync();
       await command.ExecuteNonQueryAsync();

    return (int)outputParam.Value;
        }

  public async Task<bool> UpdateAsync(Prestamo entity)
        {
using var connection = (SqlConnection)_context.CreateConnection();
      using var command = new SqlCommand("sp_Prestamo_Update", connection)
       {
      CommandType = CommandType.StoredProcedure
    };

            command.Parameters.AddWithValue("@Id", entity.Id);
command.Parameters.AddWithValue("@AlumnoId", entity.AlumnoId);
       command.Parameters.AddWithValue("@EjemplarId", entity.EjemplarId);
       command.Parameters.AddWithValue("@FechaPrestamo", entity.FechaPrestamo);
command.Parameters.AddWithValue("@FechaDevolucionPrevista", entity.FechaDevolucionPrevista);
       command.Parameters.AddWithValue("@FechaDevolucionReal", (object?)entity.FechaDevolucionReal ?? DBNull.Value);
            command.Parameters.AddWithValue("@Devuelto", entity.Devuelto);
  command.Parameters.AddWithValue("@Activo", entity.Activo);

   await connection.OpenAsync();
         var result = await command.ExecuteNonQueryAsync();

 return result > 0;
        }

  public async Task<bool> DeleteAsync(int id)
 {
 using var connection = (SqlConnection)_context.CreateConnection();
 using var command = new SqlCommand("sp_Prestamo_Delete", connection)
      {
   CommandType = CommandType.StoredProcedure
            };

          command.Parameters.AddWithValue("@Id", id);

   await connection.OpenAsync();
       var result = await command.ExecuteNonQueryAsync();

        return result > 0;
     }

        public async Task<bool> MarcarComoDevueltoAsync(int id, DateTime fechaDevolucion)
        {
  using var connection = (SqlConnection)_context.CreateConnection();
       using var command = new SqlCommand("sp_Prestamo_MarcarDevuelto", connection)
    {
       CommandType = CommandType.StoredProcedure
            };

      command.Parameters.AddWithValue("@Id", id);
   command.Parameters.AddWithValue("@FechaDevolucionReal", fechaDevolucion);

            await connection.OpenAsync();
            var result = await command.ExecuteNonQueryAsync();

return result > 0;
   }

     private static Prestamo MapFromReader(IDataRecord reader)
        {
   return new Prestamo
     {
          Id = reader.GetInt32(reader.GetOrdinal("Id")),
    AlumnoId = reader.GetInt32(reader.GetOrdinal("AlumnoId")),
           EjemplarId = reader.GetInt32(reader.GetOrdinal("EjemplarId")),
       FechaPrestamo = reader.GetDateTime(reader.GetOrdinal("FechaPrestamo")),
 FechaDevolucionPrevista = reader.GetDateTime(reader.GetOrdinal("FechaDevolucionPrevista")),
     FechaDevolucionReal = reader.IsDBNull(reader.GetOrdinal("FechaDevolucionReal")) 
   ? null 
       : reader.GetDateTime(reader.GetOrdinal("FechaDevolucionReal")),
              Devuelto = reader.GetBoolean(reader.GetOrdinal("Devuelto")),
      Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
    };
   }
    }
}
