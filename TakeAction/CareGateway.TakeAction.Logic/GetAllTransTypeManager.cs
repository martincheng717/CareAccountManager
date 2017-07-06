using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using CareGateway.TakeAction.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;

namespace CareGateway.TakeAction.Logic
{
    public class GetAllTransTypeManager : ITakeAction<GetAllTransTypeResponse, GetAllTransTypeRequest>
    {
        public ICRMCoreService CRMCoreService { get; set; }

        /// <summary>
        /// Call CRM core service to get transType
        /// </summary>
        /// <param name="request">Pass in AccountIdentifier</param>
        /// <returns>
        /// OK: 200
        /// BadRequest: 40000
        /// ExternalError: 42200
        /// NotFound: 40400
        /// InternalServerError: 50000
        /// </returns>
        public async Task<GetAllTransTypeResponse> Execute(GetAllTransTypeRequest request)
        {
            var logObject = new Dictionary<string, object>
            {
                { "AccountIdentifier", request.AccountIdentifier }
            };
            try
            {
                if (string.IsNullOrWhiteSpace(request.AccountIdentifier))
                {
                    throw new BadRequestException("Required field AccountIdentifier is empty",
                        new LogObject(request.AccountIdentifier, null));
                }

                var response = new GetAllTransTypeResponse() { CreditTransType = new List<Model.TransType>(), DebitTransType = new List<Model.TransType>() };
                var rsp = await CRMCoreService.GetAllTransType(new External.Model.Request.GetAllTransTypeRequest()
                {
                    AccountIdentifier = request.AccountIdentifier
                });

                MappingResponse(response, rsp);
                return response;
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing GetAllTransType",
                    new LogObject("GetAllTransTypeManager", logObject), ex);
            }
        }

        /// <summary>
        /// Mapping core reponse into contract response
        /// Core format: 
        /// TransType: [{ "TransCodeDescription" : "Balance Transfer FUNDING", "GDTransCode":1, "TransCodeCreditDebit" : "C", "GDTransactionClass": "108-005" ...},
        ///             { "TransCodeDescription" :"Customer Courtesy", "GDTransCode": 2,  "TransCodeCreditDebit" : "C", "GDTransactionClass": "108-005" ...}, 
        ///             { "TransCodeDescription" :"Dispute Immediate Resolution",  "GDTransCode":3,  "TransCodeCreditDebit" : "C", "GDTransactionClass": "108-005" ...},
        ///             ...
        ///             { "TransCodeDescription" :"Balance Inquiry Fee",  "GDTransCode":8,  "TransCodeCreditDebit" : "D", "GDTransactionClass": "108-005" ...},
        ///             { "TransCodeDescription" :"Funding Adjustment", "GDTransCode": 9,  "TransCodeCreditDebit" : "D", "GDTransactionClass": "108-005" ...}, 
        ///             { "TransCodeDescription" :"Lost/Stolen Repl Card Fee", "GDTransCode": 10,  "TransCodeCreditDebit" : "D", "GDTransactionClass": "108-005" ...},
        ///             ...]
        /// Return format:
        /// {
        ///     CreditTransType: [{ "TransCodeDescription" : "Balance Transfer FUNDING", "GDTransCode":1, "GDTransactionClass": "108-005"},
        ///                       { "TransCodeDescription" :"Customer Courtesy", "GDTransCode": 2, "GDTransactionClass": "108-005"}, 
        ///                       { "TransCodeDescription" :"Dispute Immediate Resolution",  "GDTransCode":3, "GDTransactionClass": "108-005"},
        ///                      ...],
        ///     DebitTransType:  [{ "TransCodeDescription" :"Balance Inquiry Fee",  "GDTransCode":8, "GDTransactionClass": "108-005"},
        ///                       { "TransCodeDescription" :"Funding Adjustment", "GDTransCode": 9, "GDTransactionClass": "108-005"}, 
        ///                       { "TransCodeDescription" :"Lost/Stolen Repl Card Fee", "GDTransCode": 10, "GDTransactionClass": "108-005"},
        ///                      ...]
        /// }
        /// </summary>
        /// <param name="response"></param>
        /// <param name="coreResponse"></param>
        private static void MappingResponse(GetAllTransTypeResponse response,
            External.Model.Response.GetAllTransTypeResponse coreResponse)
        {
            // Core service response 404
            if (coreResponse == null)
            {
                throw new NotFoundException("No record found");
            }

            if (coreResponse.listTransType == null || coreResponse.listTransType.Count == 0)
            {
                return;
            }

            foreach(var tran in coreResponse.listTransType)
            {
                var tranType = new Model.TransType()
                {
                    GDTransCode = tran.GDTransCode,
                    TransCodeDescription = tran.TransCodeDescription,
                    GDTransactionClass = tran.GDTransactionClass
                };
                
                // Credit
                if (tran.TransCodeCreditDebit == "C")
                {
                    response.CreditTransType.Add(tranType);
                }
                else if (tran.TransCodeCreditDebit == "D") // Debit
                {
                    response.DebitTransType.Add(tranType);
                }
            }
        }
    }
}
