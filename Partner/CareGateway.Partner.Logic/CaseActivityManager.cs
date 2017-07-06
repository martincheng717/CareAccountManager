using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model.Request;
using CareGateway.Partner.Model;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;

namespace CareGateway.Partner.Logic
{
    public class CaseActivityManager:IPartner<List<CaseActivityRequest>>
    {
        public IPartnerService PartnerService { get; set; }

        private static readonly ILogger Log = Gdot.Care.Common.Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public async Task Execute(List<CaseActivityRequest> request)
        {
            Task.Run(()=>HandleCaseActivity(request));
        }
        /// <summary>
        /// HandleCaseActivity
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task HandleCaseActivity(List<CaseActivityRequest> request)
        {
            foreach (var activity in request)
            {
                await CallPartnerCaseActivity(activity);
            }
        }
        /// <summary>
        /// CallPartnerCaseActivity
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        private async Task CallPartnerCaseActivity(CaseActivityRequest activity)
        {
            var logDic = new Dictionary<string, object>
                    {
                        {"PartnerCaseToken", activity.CaseNo},
                        {"CaseID", activity.PartnerCaseNo},
                        {"PartnerCaseStatusKey", activity.ParentCaseStatus.ToString()},
                        {"PartnerCaseTypeKey", activity.PartnerCaseType?.ToString()},
                    };
            try
            {
                var partnerRequest = new PartnerCaseActivityRequest();
                partnerRequest.Header=new RequestHeader()
                {
                    RequestId = Guid.NewGuid().ToString(),
                };
                partnerRequest.CaseId = activity.PartnerCaseNo;
                partnerRequest.PartnerCaseId = activity.CaseNo;
                partnerRequest.Status = activity.ParentCaseStatus.ToString();
                if (activity.PartnerCaseType.HasValue)
                {
                    partnerRequest.CaseType = activity.PartnerCaseType.ToString();
                }
                //To do call Partner API
                await PartnerService.PublishCaseStatus(partnerRequest);
            }
            catch (Exception ex)
            {
                logDic.Add("Exception", ex.Message);
                Log.Error(new LogObject("CaseActivityManager_PartnerService", logDic));
            }
        }
        
    }
}
