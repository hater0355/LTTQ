using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongRapChieuPhim.Classes
{
    public sealed record DbArg(string Name, object? Value);
    public sealed class DuplicateRecordException : Exception
    {
        public DuplicateRecordException(string message, Exception innerException) : base(message, innerException) { }
    }
    public sealed record TransactionCommand(string Sql, DbArg[] Parameters, int? ExpectedRows = null);

    /// <summary>ADO.NET access layer for SQL Server.</summary>
    public static class DataProcesser
    {
        public static string ConnectionString { get; set; } =
            Environment.GetEnvironmentVariable("CINEMA_DB_CONNECTION") ??
            @"Server=.;Database=NHHCinemaDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5;";

        public static bool TestConnection(out string errorMessage)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                errorMessage = string.Empty;
                return true;
            }
            catch (Exception firstError)
            {
                string[] fallbacks =
                {
                    @"Server=localhost;Database=NHHCinemaDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;",
                    @"Server=.\MSSQLSERVER01;Database=NHHCinemaDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;",
                    @"Server=(local);Database=NHHCinemaDB;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;"
                };

                foreach (string fallback in fallbacks)
                {
                    try
                    {
                        using var connection = new SqlConnection(fallback);
                        connection.Open();
                        ConnectionString = fallback;
                        errorMessage = string.Empty;
                        return true;
                    }
                    catch { }
                }

                errorMessage = firstError.Message;
                return false;
            }
        }

        public static DataTable Query(string sql, params DbArg[] parameters)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(sql, connection);
            AddParameters(command, parameters);
            using var adapter = new SqlDataAdapter(command);
            var result = new DataTable();
            adapter.Fill(result);
            return result;
        }

        public static int Execute(string sql, params DbArg[] parameters)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(sql, connection);
            AddParameters(command, parameters);
            connection.Open();
            try { return command.ExecuteNonQuery(); }
            catch (SqlException ex) when (ex.Number is 2601 or 2627)
            {
                throw new DuplicateRecordException("Bản ghi đã tồn tại.", ex);
            }
        }

        public static void ExecuteTransaction(IReadOnlyList<(string Sql, DbArg[] Parameters)> statements)
            => ExecuteTransaction(statements.Select(s => new TransactionCommand(s.Sql, s.Parameters)).ToArray());

        public static void ExecuteTransaction(IReadOnlyList<TransactionCommand> statements, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction(isolationLevel);
            try
            {
                foreach (var statement in statements)
                {
                    using var command = new SqlCommand(statement.Sql, connection, transaction);
                    AddParameters(command, statement.Parameters);
                    int affected = command.ExecuteNonQuery();
                    if (statement.ExpectedRows.HasValue && affected != statement.ExpectedRows.Value)
                        throw new InvalidOperationException("Dữ liệu đã thay đổi hoặc không còn hợp lệ. Vui lòng tải lại và thử lại.");
                }
                transaction.Commit();
            }
            catch
            {
                try { transaction.Rollback(); } catch { }
                throw;
            }
        }

        public static T ExecuteInTransaction<T>(Func<SqlConnection, SqlTransaction, T> operation, IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction(isolationLevel);
            try
            {
                T result = operation(connection, transaction);
                transaction.Commit();
                return result;
            }
            catch
            {
                try { transaction.Rollback(); } catch { }
                throw;
            }
        }

        public static long NextSaleSequence()
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand("SELECT NEXT VALUE FOR dbo.SeqHoaDon", connection);
            connection.Open();
            return Convert.ToInt64(command.ExecuteScalar());
        }

        public static object? Scalar(string sql, params DbArg[] parameters)
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(sql, connection);
            AddParameters(command, parameters);
            connection.Open();
            return command.ExecuteScalar();
        }

        private static void AddParameters(SqlCommand command, IEnumerable<DbArg> parameters)
        {
            foreach (var parameter in parameters)
                command.Parameters.AddWithValue(parameter.Name, parameter.Value ?? DBNull.Value);
        }
    }
}
