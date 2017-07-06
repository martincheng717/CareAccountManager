using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CareGateway.Sfdc.Logic.Tasks;
using CareGateway.Sfdc.Model;
using CareGateway.Sfdc.Model.Enum;
using CareGateway.Sfdc.Model.Salesforce;
using CareGateway.External.Client.Interfaces;
using CareGateway.External.Model;
using CareGateway.External.Model.Data;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;
using System;
using CareGateway.Sfdc.Logic.Salesforce;
using CareGateway.Sfdc.Logic.CaseService;

namespace CareGateway.Sfdc.Logic
{
    public class CaseManager :  ICaseManager
    {
        private const string SObjectName = "Case";
        private static readonly MapperConfiguration MapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<CaseEx, Case>());
        public IRiskService RiskService { get; set; }
        public IEnumerable<ICaseTask<CaseEx>> Tasks { get; set; }
        public ICaseRepository CaseRepository { get; set; }

        public async Task<CaseResponse> Create(CaseEx request)
        {
            await CaseRepository.PreProcessing(request, Tasks, CaseActionEnum.Create);
            var mapper = MapperConfig.CreateMapper();
            var caseObj = mapper.Map<Case>(request);
            var logData = new Dictionary<string, object> { { "RecordType", request.RecordType } };
            var caseResponse = await CaseRepository.CreateAsync(SalesforceEventTypeEnum.CreateCase, SObjectName, caseObj, logData);
            return caseResponse;
        }

        public async Task<CaseResponse> Update(CaseEx request)
        {
            await CaseRepository.PreProcessing(request, Tasks, CaseActionEnum.Update);
            var mapper = MapperConfig.CreateMapper();
            var caseObj = mapper.Map<Case>(request);
            var logData = new Dictionary<string, object> { { "RecordType", request.RecordType } };
            var caseResponse = await CaseRepository.UpdateAsync(SalesforceEventTypeEnum.UpdateCase, SObjectName, request.Id, caseObj, logData);
            return caseResponse;
        }

        public async Task UpdateOFACStatus(UpdateOFACStatusRequest request)
        {
            try
            {
                await RiskService.UpdateOFACStatus(new External.Model.Request.UpdateOFACStatusRequest()
                {
                    AccountIdentifier = request.AccountIdentifier,
                    IsOfacMatch = request.IsOfacMatch,
                    CaseNumber = request.CaseNumber
                });
            }
            catch (GdErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new GdErrorException(
                    "Error when executing UpdateOFACStatus",
                    new LogObject("CaseManager",
                        new Dictionary<string, object> { { "AccountIdentifier", request.AccountIdentifier } }), ex);
            }
        }
    }
}
