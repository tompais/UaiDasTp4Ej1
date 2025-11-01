using System.Data;
using ABS;
using CONTEXT;
using DOM;
using Microsoft.Data.SqlClient;

namespace REPO
{
    public class EjemplarRepository : IEjemplarRepository
    {
        private readonly DatabaseContext _context;

    public EjemplarRepository(DatabaseContext context)
        {
 _context = context ?? throw new ArgumentNullException(nameof(context));
   }

        public async Task<IEnumerable<Ejemplar>> GetAllAsync()
   {
          var ejemplares = new List<Ejemplar>();
       
         using var connection = (SqlConnection)_context.CreateConnection();
     using var command = new SqlCommand("sp_Ejemplar_GetAll", connection)
 {
             CommandType = CommandType.StoredProcedure
    };

            await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();
   
    while (await reader.ReadAsync())
            {
          ejemplares.Add(MapFromReader(reader));
            }

            return ejemplares;
 }

        public async Task<Ejemplar?> GetByIdAsync(int id)
   {
       using var connection = (SqlConnection)_context.CreateConnection();
         using var command = new SqlCommand("sp_Ejemplar_GetById", connection)
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

     public async Task<IEnumerable<Ejemplar>> GetByObraIdAsync(int obraId)
   {
  var ejemplares = new List<Ejemplar>();
            
   using var connection = (SqlConnection)_context.CreateConnection();
          using var command = new SqlCommand("sp_Ejemplar_GetByObraId", connection)
            {
    CommandType = CommandType.StoredProcedure
    };

            command.Parameters.AddWithValue("@ObraId", obraId);

        await connection.OpenAsync();
     using var reader = await command.ExecuteReaderAsync();
     
     while (await reader.ReadAsync())
            {
        ejemplares.Add(MapFromReader(reader));
            }

      return ejemplares;
   }

        public async Task<IEnumerable<Ejemplar>> GetDisponiblesAsync()
        {
 var ejemplares = new List<Ejemplar>();
            
          using var connection = (SqlConnection)_context.CreateConnection();
  using var command = new SqlCommand("sp_Ejemplar_GetDisponibles", connection)
     {
   CommandType = CommandType.StoredProcedure
     };

       await connection.OpenAsync();
      using var reader = await command.ExecuteReaderAsync();
            
         while (await reader.ReadAsync())
    {
            ejemplares.Add(MapFromReader(reader));
     }

   return ejemplares;
        }

        public async Task<Ejemplar?> GetByNumeroInventarioAsync(string numeroInventario)
        {
            using var connection = (SqlConnection)_context.CreateConnection();
       using var command = new SqlCommand("sp_Ejemplar_GetByNumeroInventario", connection)
  {
         CommandType = CommandType.StoredProcedure
            };

       command.Parameters.AddWithValue("@NumeroInventario", numeroInventario);

         await connection.OpenAsync();
   using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
    {
  return MapFromReader(reader);
          }

       return null;
        }

        public async Task<int> AddAsync(Ejemplar entity)
        {
    using var connection = (SqlConnection)_context.CreateConnection();
     using var command = new SqlCommand("sp_Ejemplar_Insert", connection)
      {
                CommandType = CommandType.StoredProcedure
  };

            command.Parameters.AddWithValue("@ObraId", entity.ObraId);
       command.Parameters.AddWithValue("@NumeroInventario", entity.NumeroInventario);
            command.Parameters.AddWithValue("@Precio", entity.Precio);

        var outputParam = new SqlParameter("@Id", SqlDbType.Int)
       {
Direction = ParameterDirection.Output
            };
   command.Parameters.Add(outputParam);

            await connection.OpenAsync();
         await command.ExecuteNonQueryAsync();

            return (int)outputParam.Value;
   }

        public async Task<bool> UpdateAsync(Ejemplar entity)
        {
   using var connection = (SqlConnection)_context.CreateConnection();
        using var command = new SqlCommand("sp_Ejemplar_Update", connection)
       {
       CommandType = CommandType.StoredProcedure
   };

     command.Parameters.AddWithValue("@Id", entity.Id);
     command.Parameters.AddWithValue("@ObraId", entity.ObraId);
          command.Parameters.AddWithValue("@NumeroInventario", entity.NumeroInventario);
            command.Parameters.AddWithValue("@Precio", entity.Precio);
          command.Parameters.AddWithValue("@Disponible", entity.Disponible);
 command.Parameters.AddWithValue("@Activo", entity.Activo);

            await connection.OpenAsync();
      var result = await command.ExecuteNonQueryAsync();

  return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
      {
using var connection = (SqlConnection)_context.CreateConnection();
            using var command = new SqlCommand("sp_Ejemplar_Delete", connection)
      {
                CommandType = CommandType.StoredProcedure
            };

       command.Parameters.AddWithValue("@Id", id);

 await connection.OpenAsync();
       var result = await command.ExecuteNonQueryAsync();

            return result > 0;
    }

    private static Ejemplar MapFromReader(IDataRecord reader)
        {
            return new Ejemplar
            {
      Id = reader.GetInt32(reader.GetOrdinal("Id")),
      ObraId = reader.GetInt32(reader.GetOrdinal("ObraId")),
        NumeroInventario = reader.GetString(reader.GetOrdinal("NumeroInventario")),
 Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
      Disponible = reader.GetBoolean(reader.GetOrdinal("Disponible")),
        Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
            };
        }
    }
}
