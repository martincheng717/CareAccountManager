using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AttributeCaching;
using Gdot.Care.Common.Data;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Model;

namespace Gdot.Care.Common.Db
{
    [ExcludeFromCodeCoverage]
    public class GetClientAuthenticationSalt : ISqlCommand<string, GetClientAuthenticationSaltInput>
    {
        private const string CommandText = "GetClientAuthenticationSalt";
        [Cacheable(Days = 1)]
        private async Task<string> GetSalt(int clientAuthenticationPartnerKey, string userName)
        {
            using (var db = new SqlDatabaseEx(CommandText))
            {
                db.AddInParameter(db.Command, "@pClientAuthenticationPartnerKey", DbType.Int32, clientAuthenticationPartnerKey);
                db.AddInParameter(db.Command, "@pUserName", DbType.String, userName);

                var dr = await db.ExecuteReaderAsync(new Dictionary<string, object> { { "ClientAuthenticationPartnerKey", clientAuthenticationPartnerKey } });
                if (await dr.ReadAsync())
                {
                    var output = dr.GetValue<string>("Salt");
                    return output;
                }
                return string.Empty;
            }
        }
        public async Task<string> ExecuteAsync(GetClientAuthenticationSaltInput input)
        {
            return await GetSalt(input.ClientAuthenticationPartnerKey, input.UserName);
        }
    }
}
