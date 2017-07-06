using System.Collections.Generic;
using System.Linq;
using CareGateway.Sfdc.Model;
using CareGateway.Sfdc.Model.Authentication;
using CareGateway.Sfdc.Model.Enum;
using Gdot.Care.Common.Utilities;
using Newtonsoft.Json.Linq;

namespace CareGateway.Sfdc.Logic
{
    public class SfdcSetting
    {
        private static List<SalesForceCaseConfiguration> _sfdcSettings;
        private static readonly dynamic Configuration = ConfigManager.Instance.Configuration;
        public static Model.SalesForceCaseConfiguration Setting(string recordType)
        {
            if (_sfdcSettings == null)
            {
                _sfdcSettings = ((JArray)Configuration.Sfdc).ToObject<List<Model.SalesForceCaseConfiguration>>();
            }
            var sfdcInfo = _sfdcSettings?.FirstOrDefault(a => a.RecordType == recordType);
            return sfdcInfo;
        }
    }
}
