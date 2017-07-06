using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Request;
using CareGateway.TakeAction.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareGateway.TakeAction.Logic
{
    public class SendEmailTriggerManager : ITakeAction<SendEmailTriggerReqeust>
    {
        public ICRMCoreService CRMCoreService { get; set; }
        public async Task Execute(SendEmailTriggerReqeust request)
        {
            var logObject = new Dictionary<string, object> {
                { "AccountIdentifer", request.AccountIdentifier },
                { "Template", request.TemplateName}
            };
            foreach (var item in request.DynamicElements)
            {
                logObject.Add(item.Key, item.Value);
            }
            try
            {
                var req = new SendEmailRequest
                {
                    AccountIdentifier = request.AccountIdentifier,
                    TemplateName = request.TemplateName,
                    DynamicElements = request.DynamicElements
                };
                await CRMCoreService.SendEmail(req);
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing SendEmailTrigger",
                    new LogObject("SendEmailTriggerManager", logObject), ex);
            }

        }
    }


}
