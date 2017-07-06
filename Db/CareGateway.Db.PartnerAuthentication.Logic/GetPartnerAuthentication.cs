using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AttributeCaching;
using CareGateway.Db.PartnerAuthentication.Model;
using Gdot.Care.Common.Data;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;

namespace CareGateway.Db.PartnerAuthentication.Logic
{
    public class GetPartnerAuthentication : ISqlCommand<GetPartnerAuthenticationOutput, GetPartnerAuthenticationInput>
    {
        private const string CommandText = "GetPartnerAuthentication";
        [Cacheable]
        public async Task<GetPartnerAuthenticationOutput> ExecuteAsync(GetPartnerAuthenticationInput input)
        {

            using (var db = new SqlDatabaseEx("FwCrmEncryptSql", CommandText))
            {
                db.AddInParameter(db.Command, "@pPartnerAuthenticationKey", DbType.Int32, input.PartnerAuthenticationKey);
                var output = new GetPartnerAuthenticationOutput();
                var dr = await db.ExecuteReaderAsync(new Dictionary<string, object> { { "Input", input } });
                if (await dr.ReadAsync())
                {
                    output.ConsumerKey = dr.GetValue<string>("ConsumerKey");
                    output.ConsumerSecret = dr.GetValue<string>("ConsumerSecret");
                    output.Domain = dr.GetValue<string>("Domain");
                    output.Login = dr.GetValue<string>("Login");
                    output.Password = dr.GetValue<string>("EncryptedPassword");
                }
                return output;
            }
        }
    }
}
