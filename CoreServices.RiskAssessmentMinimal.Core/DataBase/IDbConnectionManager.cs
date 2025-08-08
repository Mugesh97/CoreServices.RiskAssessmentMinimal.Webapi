using Npgsql;
using System.Data;


namespace CoreServices.RiskAssessmentMinimal.Implementation.DataBase
{
    public interface IDbConnectionManager : IDisposable
    {
        Task OpenConnectionAsync();
        Task CloseConnectionAsync();
        Task<int> ExecuteNonQueryAsync(string query, params NpgsqlParameter[] parameters);
        Task<IDataReader> ExecuteReaderAsync(string query, params NpgsqlParameter[]? parameters);
        Task<object?> ExecuteScalarAsync(string query, IEnumerable<NpgsqlParameter> parameters);
    }
}
