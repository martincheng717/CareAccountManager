using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CareGateway.Db.Note.Model;
using CareGateway.Db.QMaster.Model;
using CareGateway.QMaster.Model;
using Gdot.Care.Common.Extension;
using Gdot.Care.Common.Interface;
using Gdot.Care.Common.Logging;
using Gdot.Care.Common.Model;

namespace CareGateway.QMaster.Logic
{
    public class CallTransferManager: IQMasterManager<CallTransferResponse,CallTransferRequest>
    {
        public ISqlCommand<SortedList<int, PartnerCallTypeOutput>> GetAllPartnerCaseType { get; set; }
        public ISqlCommand<CallTransferOutput, CallTransferInput> CallTransfer { get; set; }
        public ISqlCommand<QMasterInfoOutput, GetQMasterbyPartnerCaseNoInput> GetQMasterByWaveCaseNo { get; set; }
        public ISqlCommand<AddNoteOutput, AddNoteInput> AddNote { get; set; }

        private static readonly ILogger Log = Gdot.Care.Common.Logging.Log.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<CallTransferResponse> Execute(CallTransferRequest request)
        {
            var response = new CallTransferResponse()
            {
                StatusCode = HttpStatusCode.OK.ToIntegerString()
            };
           
            try
            {
                
                var hasOriginatorCaseType = false;
                var getAllPartnerCaseType =  GetAllPartnerCaseType.ExecuteAsync();
                var getQMasterByWaveCaseNo =  GetQMasterByWaveCaseNo.ExecuteAsync(new GetQMasterbyPartnerCaseNoInput() { PartnerCaseNo = request.OriginatorCaseNo });
                var getAllCaseTypeResult = await getAllPartnerCaseType;
                var getQMasterByWaveCaseNoResult = await getQMasterByWaveCaseNo;

                if (getQMasterByWaveCaseNoResult != null)
                {
                    response.StatusCode = HttpStatusCode.Created.ToIntegerString();
                }
                int? caseTypeKey = null;
                if (getAllCaseTypeResult != null)
                {
                    caseTypeKey =
                        getAllCaseTypeResult.Values.SingleOrDefault(m => m.PartnerCallType == request.IssueCode)?
                            .PartnerCallTypeKey;
                    hasOriginatorCaseType = getAllCaseTypeResult.Values.Count(m => m.PartnerCallType==request.IssueCode)>0;
                }

                if (!hasOriginatorCaseType)
                {
                    response.StatusCode = HttpStatusCode.NonAuthoritativeInformation.ToIntegerString();
                }

                var callTransfer = await CallTransfer.ExecuteAsync(new CallTransferInput()
                                {
                                    PartnerCaseNo = request.OriginatorCaseNo,
                                    AccountIdentifier = request.AccountIdentifier,
                                    PartnerCallTypeKey = caseTypeKey
                                });

                if (callTransfer != null)
                {
                    response.SessionId = "000"+callTransfer.QMasterKey.ToString();
                }
                else
                {
                    response.StatusCode = HttpStatusCode.InternalServerError.ToIntegerString();
                }
                if (!string.IsNullOrEmpty(request.AccountIdentifier))
                {
                    try
                    {

                        await AddNote.ExecuteAsync(new AddNoteInput()
                        {
                            Note =
                                $"Call transfer AccountIdentifier:{request.AccountIdentifier}, OriginatorCaseNo:{request.OriginatorCaseNo},IssueCode:{request.IssueCode},SessionId:{response.SessionId}",
                            AccountIdentifier = new Guid(request.AccountIdentifier),
                            UserFullName = "Partner"
                        });

                    }
                    catch (Exception e)
                    {
                        Log.Error(new LogObject("CareQMaster_CallTransferAsync", new Dictionary<string, object>
                        {
                            {"AccountIdentifier", request.AccountIdentifier},
                            {"OriginatorCaseNo", request.OriginatorCaseNo},
                            {"IssueCode", request.IssueCode},
                            {"SessionId", response.SessionId},
                            {"Error", e.StackTrace}
                        }));
                    }
                }
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError.ToIntegerString();
                Log.Error(new LogObject("CareQMaster_CallTransferAsync", new Dictionary<string, object>
                        {
                            {"AccountIdentifier", request.AccountIdentifier},
                            {"OriginatorCaseNo", request.OriginatorCaseNo },
                            {"IssueCode", request.IssueCode},
                            {"SessionId", response.SessionId},
                            {"Error", e.StackTrace}
                        }));
            }
            return response;
        }
    }
}
