using Npgsql;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CoreServices.RiskAssessmentMinimal.Implementation.DataBase
{
    [ExcludeFromCodeCoverage]
    public class DbConnectionManager(string? connectionString) : IDbConnectionManager
    {
        private readonly NpgsqlConnection _connection = new(connectionString);
        private NpgsqlCommand? _command;
        private bool _disposed;

        public async Task OpenConnectionAsync()
        {
            ThrowIfDisposed();

            if (_connection.State != ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
        }

        public async Task CloseConnectionAsync()
        {
            ThrowIfDisposed();

            if (_connection.State == ConnectionState.Open)
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string query, params NpgsqlParameter[] parameters)
        {
            ThrowIfDisposed();

            _command = new NpgsqlCommand(query, _connection);
            _command.Parameters.AddRange(parameters);
            return await _command.ExecuteNonQueryAsync();
        }

        public async Task<IDataReader> ExecuteReaderAsync(string query, params NpgsqlParameter[]? parameters)
        {
            ThrowIfDisposed();

            _command = new NpgsqlCommand(query, _connection);
            if (parameters != null) _command.Parameters.AddRange(parameters);

            return new DataReader(await _command.ExecuteReaderAsync());
        }

        public async Task<object?> ExecuteScalarAsync(string query, IEnumerable<NpgsqlParameter> parameters)
        {
            ThrowIfDisposed();

            using var command = new NpgsqlCommand(query, _connection);
            command.Parameters.AddRange(parameters.ToArray());
            return await command.ExecuteScalarAsync();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(DbConnectionManager));
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _command?.Dispose();
                    _connection.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
