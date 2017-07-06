using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AttributeCaching;
using CareGateway.Db.QMaster.Model;
using Gdot.Care.Common.Data;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;

namespace CareGateway.Db.QMaster.Logic
{
    public class GetAllPartnerCallType : ISqlCommand<SortedList<int, PartnerCallTypeOutput>>
    {
        private const string CommandText = "GetAllPartnerCallType";

        [Cacheable(Days = 1)]
        public async Task<SortedList<int, PartnerCallTypeOutput>> ExecuteAsync()
        {
            using (var db = new SqlDatabaseEx(CommandText))
            {
                var output = new SortedList<int, PartnerCallTypeOutput>();
                var dr = await db.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    var partnerCallType = new PartnerCallTypeOutput
                    {
                        PartnerCallTypeKey = dr.GetValue<int>("PartnerCallTypeKey"),
                        PartnerCallType = dr.GetValue<string>("PartnerCallType"),
                        PartnerCallTypeDescription = dr.GetValue<string>("PartnerCallTypeDescription")
                    };
                    output.Add(partnerCallType.PartnerCallTypeKey, partnerCallType);
                }
                return output;
            }
        }
    }
}
