using CareGateway.External.Client.Interfaces;
using CareGateway.Transaction.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.Transaction.Logic
{
    public class ReverseAuthorizationManager : ITransaction<AuthorizationReversalRequest>
    {
        public ICRMCoreService CRMCoreService { get; set; }
        public async Task Execute(AuthorizationReversalRequest request)
        {
            try
            {
                await CRMCoreService.ReverseAuthorization(new External.Model.Request.ReverseAuthorizationRequest
                {
                    AccountIdentifier = request.AccountIdentifier,
                    AuthorizedTransactionKey = request.AuthorizedTransactionKey
                });
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing ReverseAuthorization",
                    new LogObject("ReverseAuthorizationManager",
                        new Dictionary<string, object>
                            {
                                { "AccountIdentifier", request.AccountIdentifier },
                                { "AuthorizedTransactionKey", request.AuthorizedTransactionKey}
                            }), ex);
            }
        }
    }
}
