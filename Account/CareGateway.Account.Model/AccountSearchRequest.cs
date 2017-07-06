using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Account.Model.Enum;
using Gdot.Care.Common.Attributes;

namespace CareGateway.Account.Model
{
    public class AccountSearchRequest
    {
        [EnumDataType(typeof(SearchOptionEnum))]
        [Required(AllowEmptyStrings = false)]
        public SearchOptionEnum? Option { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Restricted]
        public string Value { get; set; }
    }
}
