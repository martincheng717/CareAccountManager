using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Model.Data;

namespace CareGateway.External.Model.Response
{
    [ExcludeFromCodeCoverage]
    public class GetDatesAvailableResponse:ResponseBase
    {
        public AvailableDates AvailableDates { get; set; }
    }
}
