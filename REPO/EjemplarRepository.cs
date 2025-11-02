using ABS;
using CONTEXT;
using DOM;
using Microsoft.Data.SqlClient;
using System.Data;

namespace REPO
{
    public class EjemplarRepository(DatabaseContext context) : IEjemplarRepository
    {
     public IEnumerable<Ejemplar> GetAll()
        {
    var ejemplares = new List<Ejemplar>();

        using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Ejemplar_GetAll", connection)
     {
        CommandType = CommandType.StoredProcedure
         };

       connection.Open();
  using var reader = command.ExecuteReader();

       while (reader.Read())
   {
         ejemplares.Add(MapFromReader(reader));
          }

 return ejemplares;
        }

        public Ejemplar? GetById(int id)
      {
     using var connection = (SqlConnection)context.CreateConnection();
   using var command = new SqlCommand("sp_Ejemplar_GetById", connection)
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

        public IEnumerable<Ejemplar> GetByObraId(int obraId)
      {
      var ejemplares = new List<Ejemplar>();

     using var connection = (SqlConnection)context.CreateConnection();
        using var command = new SqlCommand("sp_Ejemplar_GetByObraId", connection)
      {
     CommandType = CommandType.StoredProcedure
  };

            command.Parameters.AddWithValue("@ObraId", obraId);

            connection.Open();
            using var reader = command.ExecuteReader();

 while (reader.Read())
            {
   ejemplares.Add(MapFromReader(reader));
         }

         return ejemplares;
}

        public IEnumerable<Ejemplar> GetDisponibles()
        {
       var ejemplares = new List<Ejemplar>();

            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Ejemplar_GetDisponibles", connection)
     {
      CommandType = CommandType.StoredProcedure
            };

            connection.Open();
     using var reader = command.ExecuteReader();

            while (reader.Read())
      {
      ejemplares.Add(MapFromReader(reader));
            }

            return ejemplares;
        }

    public Ejemplar? GetByNumeroInventario(string numeroInventario)
        {
   using var connection = (SqlConnection)context.CreateConnection();
          using var command = new SqlCommand("sp_Ejemplar_GetByNumeroInventario", connection)
  {
  CommandType = CommandType.StoredProcedure
};

      command.Parameters.AddWithValue("@NumeroInventario", numeroInventario);

            connection.Open();
         using var reader = command.ExecuteReader();

         if (reader.Read())
            {
     return MapFromReader(reader);
          }

          return null;
        }

        public int Add(Ejemplar entity)
        {
   using var connection = (SqlConnection)context.CreateConnection();
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

            connection.Open();
            command.ExecuteNonQuery();

  return (int)outputParam.Value;
  }

        public bool Update(Ejemplar entity)
        {
            using var connection = (SqlConnection)context.CreateConnection();
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

            connection.Open();
var result = command.ExecuteNonQuery();

            return result > 0;
      }

  public bool Delete(int id)
        {
     using var connection = (SqlConnection)context.CreateConnection();
       using var command = new SqlCommand("sp_Ejemplar_Delete", connection)
 {
       CommandType = CommandType.StoredProcedure
            };

         command.Parameters.AddWithValue("@Id", id);

  connection.Open();
            var result = command.ExecuteNonQuery();

            return result > 0;
        }

public Ejemplar MapFromReader(IDataRecord reader) => new()
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
