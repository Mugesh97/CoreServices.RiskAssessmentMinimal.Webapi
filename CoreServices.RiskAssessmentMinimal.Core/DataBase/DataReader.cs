using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace CoreServices.RiskAssessmentMinimal.Implementation.DataBase
{
    [ExcludeFromCodeCoverage]
    public class DataReader : IDataReader, IAsyncDisposable
    {
        private readonly NpgsqlDataReader _reader;
        private bool _disposed;

        public DataReader(NpgsqlDataReader reader)
        {
            _reader = reader;
        }

        public async Task<bool> ReadAsync()
        {
            return await _reader.ReadAsync();
        }

        public Guid GetGuid(int ordinal)
        {
            return _reader.GetGuid(ordinal);
        }

        public string GetString(int ordinal)
        {
            return _reader.GetString(ordinal);
        }

        public bool IsDbNull(int ordinal)
        {
            return _reader.IsDBNull(ordinal);
        }

        public DateTime GetDateTime(int ordinal)
        {
            return _reader.GetDateTime(ordinal);
        }

        public bool GetBoolean(int ordinal)
        {
            return _reader.GetBoolean(ordinal);
        }

        // Synchronous Dispose
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _reader?.Dispose();
                }

                _disposed = true;
            }
        }

        // Async Dispose (C# 8.0+)
        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                await _reader.DisposeAsync();
                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
