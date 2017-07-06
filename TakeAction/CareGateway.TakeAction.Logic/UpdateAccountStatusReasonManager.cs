using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Data;
using CareGateway.External.Model.Request;
using CareGateway.TakeAction.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.TakeAction.Logic
{
    public class UpdateAccountStatusReasonManager
        : ITakeAction<UpdateAccountStatusReasonRequest>
    {
        public ICRMCoreService CRMCoreService { get; set; }
        public async Task Execute(UpdateAccountStatusReasonRequest request)
        {
            var logObject = new Dictionary<string, object> {
                { "AccountIdentifer", request.AccountIdentifier },
                { "StatusReason", request.ReasonKey},
                { "Status", request.Status}
            };
            try
            {
                var req = new UpdAccountStatusReasonRequest
                {
                    AccountIdentifier = request.AccountIdentifier,
                };
                if (!ValidateStatusAndStatusReason(request.ReasonKey, request.Status, req))
                {
                    throw new BadRequestException($"Required field Status/ReasonKey is invalid",
                        new LogObject("UpdateAccountStatusReasonManager", logObject));
                }

                await CRMCoreService.UpdAccountStatusReason(req);
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing UpdateAccountStatus",
                    new LogObject("UpdateAccountStatusReasonManager", logObject), ex);
            }
        }

        private bool ValidateStatusAndStatusReason(string reasonKey, string status, UpdAccountStatusReasonRequest req)
        {
            try
            {
                if (String.IsNullOrEmpty(reasonKey) 
                    && status == null)
                {
                    return false;
                }

                if (!String.IsNullOrEmpty(reasonKey) )
                {
                    req.AccountStatusReason = (StateReason) int.Parse(reasonKey);                                                
                }

                if (!String.IsNullOrEmpty(status))
                {
                     req.Status = status.Trim();
                }                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
