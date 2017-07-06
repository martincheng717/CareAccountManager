using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Account.Model;
using CareGateway.Account.Model.Enum;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using CareGateway.External.Model.Request;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;
using Newtonsoft.Json;

namespace CareGateway.Account.Logic
{
    public class AccountSearchManager : IAccount<List<AccountSearchInfo>, AccountSearchRequest>
    {
        public ICRMCoreService CRMCoreService { get; set; }

        public async Task<List<AccountSearchInfo>> Execute(AccountSearchRequest request)
        {
            var logObject = new Dictionary<string, object> {{"Search Option", request.Option.Value}};
            try
            {
                var response = new  List<AccountSearchInfo>();
                var coreSearchResults =new List<CustomerInfo>();
                switch (request.Option)
                {
                    case SearchOptionEnum.AccountNumber:
                        coreSearchResults = await CRMCoreService.GetCustomerInfoByAccountNumber(request.Value);
                        break;
                    case SearchOptionEnum.SSN:
                        coreSearchResults = await CRMCoreService.GetCustomerInfoBySSN(request.Value);
                        break;
                    case SearchOptionEnum.CustomerInfo:
                        var coreRequest = new SearchAccountByDetailRequest();
                        if (!ProcessCustomerDetailsRequest(ref coreRequest, request.Value))
                        {
                            throw new GdErrorException("Error while executing AccountSearch by customer detailes",
                                new LogObject("AccountSearchManager_AccountSearch",
                                    new Dictionary<string, object>
                                    {
                                        {"Option", request.Option.ToString()},
                                        {"Value", ""}
                                    }));
                        }
                        coreSearchResults = await CRMCoreService.GetCustomerInfoByCustomerDetail(coreRequest);
                        break;
                }

                if (coreSearchResults == null|| !coreSearchResults.Any())
                {
                    throw new NotFoundException("No record found",
                        new LogObject("AccountSearch",logObject));
                }

                MappingResponse(response, coreSearchResults);

                return response;
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing AccountSearch",
                    new LogObject("AccountSearchManager", logObject), ex);
            }
        }

        private static void MappingResponse(List<AccountSearchInfo> response, List<CustomerInfo> coreSearchResults)
        {
            response.AddRange(coreSearchResults.Select(customerInfo => new AccountSearchInfo()
            {
                LastName = customerInfo.LastName,
                AccountIdentifier = customerInfo.AccountIdentifier,
                AccountState = customerInfo.AccountState,
                FirstName = customerInfo.FirstName,
                Last4SSN = customerInfo.Last4SSN,
                AccountNumber = customerInfo.AccountNumber
            }));
        }


        private static bool ProcessCustomerDetailsRequest(ref SearchAccountByDetailRequest request, string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            try
            {
                var conditionsSort = JsonConvert.DeserializeObject<SortedList<string, string>>(value)
                    .Where(pair => pair.Value.Trim() != "").ToDictionary(pair => pair.Key,
                                                                    pair => pair.Value);

                if (conditionsSort.Count >= 2)
                {
                    foreach (var condition in conditionsSort)
                    {
                        typeof(SearchAccountByDetailRequest).GetProperty(condition.Key).SetValue(
                            request,
                            condition.Key != "DOB"
                                ? (object)condition.Value
                                : DateTime.ParseExact(condition.Value, "MMddyyyy",
                                    System.Globalization.CultureInfo.InvariantCulture,
                                    System.Globalization.DateTimeStyles.None).ToString("yyyy-MM-dd"));
                    }
                    return !string.IsNullOrEmpty(request.LastName);
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
