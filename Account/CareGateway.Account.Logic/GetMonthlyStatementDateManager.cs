using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Account.Model;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;

namespace CareGateway.Account.Logic
{
    public class GetMonthlyStatementDateManager : IAccount<MonthlyStatementDateResponse, string>
    {
        public ICRMCoreService CRMCoreService { get; set; }
        public async Task<MonthlyStatementDateResponse> Execute(string accountIdentifier)
        {
            try
            {
                var response = new MonthlyStatementDateResponse();

                var availableDates = await CRMCoreService.GetMonthlyStatementDate(accountIdentifier);

                if (availableDates == null)
                {
                    throw new NotFoundException("No record found",
                        new LogObject("GetMonthlyStatementDateManager",
                            new Dictionary<string, object> { { "AccountIdentifier", accountIdentifier } }));
                }

                MappingResponse(response, availableDates);

                return response;
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing GetMonthlyStatementDate",
                    new LogObject("GetMonthlyStatementDateManager",
                        new Dictionary<string, object> { { "AccountIdentifier", accountIdentifier } }), ex);
            }
        }

        /// <summary>
        /// MappingResponse
        /// </summary>
        /// <param name="response"></param>
        /// <param name="availableDates"></param>
        private static void MappingResponse(MonthlyStatementDateResponse response, AvailableDates availableDates)
        {
            response.StartDate = availableDates.StartDate;
            response.EndDate = availableDates.EndDate;
        }
    }
}
