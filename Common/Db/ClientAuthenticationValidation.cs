using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Gdot.Care.Common.Data;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;
using AttributeCaching;
using Gdot.Care.Common.Model;

namespace Gdot.Care.Common.Db
{
    [ExcludeFromCodeCoverage]
    public class ClientAuthenticationValidation : ISqlCommand<int, ClientAuthenticationValidationInput>
    {
        private const string CommandText = "ClientAuthenticationValidation";

        [Cacheable(Days = 1)]
        public async Task<int> GetClientAuthenticationKey(int clientAuthenticationPartnerKey, string userName, string hashPassword)
        {
            using (var db = new SqlDatabaseEx(CommandText))
            {
                db.AddInParameter(db.Command, "@pClientAuthenticationPartnerKey", DbType.Int32, clientAuthenticationPartnerKey);
                db.AddInParameter(db.Command, "@pUserName", DbType.String, userName);
                db.AddInParameter(db.Command, "@pHashPassword", DbType.String, hashPassword);

                var dr = await db.ExecuteReaderAsync(new Dictionary<string, object> { { "ClientAuthenticationPartnerKey", clientAuthenticationPartnerKey } });
                if (await dr.ReadAsync())
                {
                    var output = dr.GetValue<int>("ClientAuthenticationKey");
                    return output;
                }
                return 0;
            }
        }

        
        public async Task<int> ExecuteAsync(ClientAuthenticationValidationInput input)
        {
            return await GetClientAuthenticationKey(input.ClientAuthenticationPartnerKey, input.UserName, input.HashPassword);
        }
    }
}
