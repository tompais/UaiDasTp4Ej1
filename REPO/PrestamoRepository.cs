using ABS;
using CONTEXT;
using DOM;
using Microsoft.Data.SqlClient;
using System.Data;

namespace REPO
{
    public class PrestamoRepository(DatabaseContext context) : IPrestamoRepository
    {
        public IEnumerable<Prestamo> GetAll()
        {
            var prestamos = new List<Prestamo>();

            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Prestamo_GetAll", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                prestamos.Add(MapFromReader(reader));
            }

            return prestamos;
        }

        public Prestamo? GetById(int id)
        {
            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Prestamo_GetById", connection)
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

        public IEnumerable<Prestamo> GetByAlumnoId(int alumnoId)
        {
            var prestamos = new List<Prestamo>();

            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Prestamo_GetByAlumnoId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@AlumnoId", alumnoId);

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                prestamos.Add(MapFromReader(reader));
            }

            return prestamos;
        }

        public IEnumerable<Prestamo> GetPrestamosActivos()
        {
            var prestamos = new List<Prestamo>();

            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Prestamo_GetActivos", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                prestamos.Add(MapFromReader(reader));
            }

            return prestamos;
        }

        public IEnumerable<Prestamo> GetPrestamosVencidos()
        {
            var prestamos = new List<Prestamo>();

            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Prestamo_GetVencidos", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                prestamos.Add(MapFromReader(reader));
            }

            return prestamos;
        }

        public int Add(Prestamo entity)
        {
            using var connection = (SqlConnection)context.CreateConnection();
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

            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputParam.Value;
        }

        public bool Update(Prestamo entity)
        {
            using var connection = (SqlConnection)context.CreateConnection();
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

            connection.Open();
            var result = command.ExecuteNonQuery();

            return result > 0;
        }

        public bool Delete(int id)
        {
            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Prestamo_Delete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            var result = command.ExecuteNonQuery();

            return result > 0;
        }

        public bool MarcarComoDevuelto(int id, DateTime fechaDevolucion)
        {
            using var connection = (SqlConnection)context.CreateConnection();
            using var command = new SqlCommand("sp_Prestamo_MarcarDevuelto", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@FechaDevolucionReal", fechaDevolucion);

            connection.Open();
            var result = command.ExecuteNonQuery();

            return result > 0;
        }

        public Prestamo MapFromReader(IDataRecord reader) => new()
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
