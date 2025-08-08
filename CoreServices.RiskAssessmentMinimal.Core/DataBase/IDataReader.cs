using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreServices.RiskAssessmentMinimal.Implementation.DataBase
{
    public interface IDataReader : IDisposable
    {
        Task<bool> ReadAsync();
        Guid GetGuid(int ordinal);
        string GetString(int ordinal);
        bool IsDbNull(int ordinal);
        DateTime GetDateTime(int ordinal);
        bool GetBoolean(int ordinal);
    }
}
