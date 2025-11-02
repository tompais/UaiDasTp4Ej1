using ABS;
using CONTEXT;
using DOM;
using Microsoft.Data.SqlClient;
using System.Data;

namespace REPO
{
    public class ObraRepository(DatabaseContext context) : IObraRepository
    {
        public IEnumerable<Obra> GetAll()
        {
            var obras = new List<Obra>();

            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Obra_GetAll", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                obras.Add(MapFromReader(reader));
            }

            return obras;
        }

        public Obra? GetById(int id)
        {
            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Obra_GetById", connection)
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

        public Obra? GetByIsbn(string isbn)
        {
            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Obra_GetByIsbn", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Isbn", isbn);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapFromReader(reader);
            }

            return null;
        }

        public int Add(Obra entity)
        {
            using var connection = (SqlConnection)context.CreateConnection();
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

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputParam.Value;
        }

        public bool Update(Obra entity)
        {
            using var connection = (SqlConnection)context.CreateConnection();
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

            connection.Open();
            var result = command.ExecuteNonQuery();

            return result > 0;
        }

        public bool Delete(int id)
        {
            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Obra_Delete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            var result = command.ExecuteNonQuery();

            return result > 0;
        }

        public Obra MapFromReader(IDataRecord reader) => new()
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
