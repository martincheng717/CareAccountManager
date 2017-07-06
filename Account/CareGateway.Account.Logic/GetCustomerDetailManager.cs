using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Account.Model;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model;
using CareGateway.External.Model.Data;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;

namespace CareGateway.Account.Logic
{
    public class GetCustomerDetailManager : IAccount<CustomerDetailResponse, string>
    {
        public ICRMCoreService CRMCoreService { get; set; }
        public async Task<CustomerDetailResponse> Execute(string accountIdentifier)
        {
            try
            {
                var response = new CustomerDetailResponse();
                //call to get routing number
                var customerDetail = await CRMCoreService.GetCustomerDetail(accountIdentifier);

                if (customerDetail == null)
                {
                    throw new NotFoundException("No record found",
                        new LogObject("GetCustomerDetailManager",
                            new Dictionary<string, object> { { "AccountIdentifier", accountIdentifier } }));
                }

                MappingResponse(response, customerDetail);

                return response;
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                                    "Error when executing GetCustomerDetail",
                                    new LogObject("GetCustomerDetailManager",
                                        new Dictionary<string, object> { { "AccountIdentifier", accountIdentifier } }), ex);
            }
        }

        /// <summary>
        /// MappingResponse
        /// </summary>
        /// <param name="response"></param>
        /// <param name="customerDetail"></param>
        private static void MappingResponse(CustomerDetailResponse response, CustomerDetail customerDetail)
        {
            response.AccountExternalID = customerDetail.AccountExternalID;
            response.SSNToken = customerDetail.SSNToken;
            response.Address = customerDetail.Address;
            response.DOB = customerDetail.DOB;
            response.FirstName = customerDetail.FirstName;
            response.Last4SSN = customerDetail.Last4SSN;
            response.LastName = customerDetail.LastName;
            response.MiddleName = customerDetail.MiddleName;
            response.CreateDate = customerDetail.CreateDate;
        }
    }
}
