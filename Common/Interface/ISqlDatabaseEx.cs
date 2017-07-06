using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Gdot.Care.Common.Interface
{
    public interface ISqlDatabaseEx
    {
        Task<DbDataReader> ExecuteReaderAsync(IDictionary<string, object> logData = null);
        Task<int> ExecuteNonQueryAsync(IDictionary<string, object> logData = null);
        DbCommand Command { get; }
    }
}
