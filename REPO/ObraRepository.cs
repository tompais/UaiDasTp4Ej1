using System.Data;
using ABS;
using CONTEXT;
using DOM;
using Microsoft.Data.SqlClient;

namespace REPO
{
    public class ObraRepository : IObraRepository
    {
        private readonly DatabaseContext _context;

        public ObraRepository(DatabaseContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Obra>> GetAllAsync()
        {
        var obras = new List<Obra>();
   
 using var connection = (SqlConnection)_context.CreateConnection();
            using var command = new SqlCommand("sp_Obra_GetAll", connection)
            {
         CommandType = CommandType.StoredProcedure
    };

            await connection.OpenAsync();
   using var reader = await command.ExecuteReaderAsync();
     
    while (await reader.ReadAsync())
     {
         obras.Add(MapFromReader(reader));
            }

  return obras;
    }

        public async Task<Obra?> GetByIdAsync(int id)
        {
     using var connection = (SqlConnection)_context.CreateConnection();
      using var command = new SqlCommand("sp_Obra_GetById", connection)
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

        public async Task<Obra?> GetByIsbnAsync(string isbn)
        {
 using var connection = (SqlConnection)_context.CreateConnection();
            using var command = new SqlCommand("sp_Obra_GetByIsbn", connection)
            {
           CommandType = CommandType.StoredProcedure
    };

   command.Parameters.AddWithValue("@Isbn", isbn);

     await connection.OpenAsync();
            using var reader = await command.ExecuteReaderAsync();

   if (await reader.ReadAsync())
       {
           return MapFromReader(reader);
            }

            return null;
   }

        public async Task<int> AddAsync(Obra entity)
        {
            using var connection = (SqlConnection)_context.CreateConnection();
          using var command = new SqlCommand("sp_Obra_Insert", connection)
   {
                CommandType = CommandType.StoredProcedure
  };

     command.Parameters.AddWithValue("@Titulo", entity.Titulo);
            command.Parameters.AddWithValue("@Autor", entity.Autor);
       command.Parameters.AddWithValue("@Editorial", entity.Editorial);
       command.Parameters.AddWithValue("@Isbn", entity.Isbn);
      command.Parameters.AddWithValue("@AnioPublicacion", entity.AnioPublicacion);
            command.Parameters.AddWithValue("@Genero", entity.Genero);

            var outputParam = new SqlParameter("@Id", SqlDbType.Int)
            {
       Direction = ParameterDirection.Output
 };
        command.Parameters.Add(outputParam);

await connection.OpenAsync();
    await command.ExecuteNonQueryAsync();

 return (int)outputParam.Value;
}

        public async Task<bool> UpdateAsync(Obra entity)
        {
       using var connection = (SqlConnection)_context.CreateConnection();
         using var command = new SqlCommand("sp_Obra_Update", connection)
       {
          CommandType = CommandType.StoredProcedure
    };

            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Titulo", entity.Titulo);
            command.Parameters.AddWithValue("@Autor", entity.Autor);
       command.Parameters.AddWithValue("@Editorial", entity.Editorial);
          command.Parameters.AddWithValue("@Isbn", entity.Isbn);
       command.Parameters.AddWithValue("@AnioPublicacion", entity.AnioPublicacion);
     command.Parameters.AddWithValue("@Genero", entity.Genero);
         command.Parameters.AddWithValue("@Activo", entity.Activo);

            await connection.OpenAsync();
   var result = await command.ExecuteNonQueryAsync();

      return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
 {
 using var connection = (SqlConnection)_context.CreateConnection();
       using var command = new SqlCommand("sp_Obra_Delete", connection)
            {
          CommandType = CommandType.StoredProcedure
 };

            command.Parameters.AddWithValue("@Id", id);

   await connection.OpenAsync();
         var result = await command.ExecuteNonQueryAsync();

            return result > 0;
        }

        private static Obra MapFromReader(IDataRecord reader)
        {
  return new Obra
            {
         Id = reader.GetInt32(reader.GetOrdinal("Id")),
  Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
      Autor = reader.GetString(reader.GetOrdinal("Autor")),
       Editorial = reader.GetString(reader.GetOrdinal("Editorial")),
          Isbn = reader.GetString(reader.GetOrdinal("Isbn")),
   AnioPublicacion = reader.GetInt32(reader.GetOrdinal("AnioPublicacion")),
   Genero = reader.GetString(reader.GetOrdinal("Genero")),
  Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
      };
        }
    }
}
