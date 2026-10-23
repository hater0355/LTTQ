using System.Data;
using Microsoft.Data.SqlClient;

namespace HeThongRapChieuPhim.Classes
{
    /// <summary>A SQL Server parameter with its database type declared explicitly.</summary>
    public sealed record DbArg(
        string Name,
        object? Value,
        SqlDbType SqlType,
        int? Size = null,
        byte? Precision = null,
        byte? Scale = null)
    {
        public static DbArg VarChar(string name, object? value, int size) => new(name, value, SqlDbType.VarChar, size);
        public static DbArg NVarChar(string name, object? value, int size) => new(name, value, SqlDbType.NVarChar, size);
        public static DbArg VarChar10(string name, object? value) => VarChar(name, value, 10);
        public static DbArg VarChar15(string name, object? value) => VarChar(name, value, 15);
        public static DbArg VarChar20(string name, object? value) => VarChar(name, value, 20);
        public static DbArg VarChar50(string name, object? value) => VarChar(name, value, 50);
        public static DbArg VarChar64(string name, object? value) => VarChar(name, value, 64);
        public static DbArg NVarChar20(string name, object? value) => NVarChar(name, value, 20);
        public static DbArg NVarChar50(string name, object? value) => NVarChar(name, value, 50);
        public static DbArg NVarChar100(string name, object? value) => NVarChar(name, value, 100);
        public static DbArg NVarChar150(string name, object? value) => NVarChar(name, value, 150);
        public static DbArg NVarChar255(string name, object? value) => NVarChar(name, value, 255);
        public static DbArg NVarChar500(string name, object? value) => NVarChar(name, value, 500);
        public static DbArg NVarChar4000(string name, object? value) => NVarChar(name, value, 4000);
        public static DbArg NVarCharMax(string name, object? value) => NVarChar(name, value, -1);
        public static DbArg Int(string name, object? value) => new(name, value, SqlDbType.Int);
        public static DbArg BigInt(string name, object? value) => new(name, value, SqlDbType.BigInt);
        public static DbArg Bit(string name, object? value) => new(name, value, SqlDbType.Bit);
        public static DbArg Date(string name, object? value) => new(name, value, SqlDbType.Date);
        public static DbArg DateTime(string name, object? value) => new(name, value, SqlDbType.DateTime);
        public static DbArg DateTime2(string name, object? value) => new(name, value, SqlDbType.DateTime2, Scale: 7);
        public static DbArg Time(string name, object? value) => new(name, value, SqlDbType.Time, Scale: 0);
        public static DbArg Decimal(string name, object? value, byte precision, byte scale) =>
            new(name, value, SqlDbType.Decimal, Precision: precision, Scale: scale);
        public static DbArg Decimal18_0(string name, object? value) => Decimal(name, value, 18, 0);
        public static DbArg Decimal18_2(string name, object? value) => Decimal(name, value, 18, 2);
        public static DbArg Decimal5_2(string name, object? value) => Decimal(name, value, 5, 2);

        public SqlParameter CreateSqlParameter()
        {
            var parameter = new SqlParameter(Name, SqlType)
            {
                Value = Value ?? DBNull.Value
            };
            if (Size.HasValue) parameter.Size = Size.Value;
            if (Precision.HasValue) parameter.Precision = Precision.Value;
            if (Scale.HasValue) parameter.Scale = Scale.Value;
            return parameter;
        }
    }
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
            LocalPaymentsSettings.ReadValue("ConnectionStrings", "CinemaDb") ??
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
                command.Parameters.Add(parameter.CreateSqlParameter());
        }
    }
}
