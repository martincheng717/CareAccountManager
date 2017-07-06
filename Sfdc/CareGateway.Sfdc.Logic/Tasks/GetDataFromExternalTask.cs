using System;
using System.Threading.Tasks;
using CareGateway.Sfdc.Model;
using CareGateway.Sfdc.Model.Enum;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;

namespace CareGateway.Sfdc.Logic.Tasks
{
    public class GetDataFromExternalTask : ICaseTask<CaseEx>
    {
        public bool IsAsync { get; set; }
        public ICaseRepository CaseRepository { get; set; }
        public GetDataFromExternalTask()
        {
            IsAsync = true;
        }
        public async Task ExecuteAsync(CaseEx request, CaseActionEnum action)
        {

            if (action == CaseActionEnum.Update && string.IsNullOrEmpty(request.Id))
            {
                request.Id = await CaseRepository.GetCaseId(request.CaseNumber);
                if (string.IsNullOrEmpty(request.Id))
                {

                    throw new NotFoundException("Cannot find the case with case number.",
                        new LogObject("GetDataFromExternalTask_UpdateExecuteAsync", null));
                }

            }
            Task<string> ownerId = null;
            if (!string.IsNullOrEmpty(request.GroupName))
            {
                ownerId = CaseRepository.GetOwnerId(request.GroupName);
            }
            else if (!string.IsNullOrEmpty(request.CaseOwner))
            {
                ownerId = CaseRepository.GetOwnerIdByUserName(request.CaseOwner);
            }
            var personAccountInput = new PersonAccountInput
            {
                FirstName = request.First_Name__c,
                LastName = request.Last_Name__c,
                AccountIdentifier = request.AccountIdentifier,
                Brand = request.Brand_OFAC__c,
                Product = "Wave"
            };
            //await GetCustomerInfo(personAccountInput);//Get Customer info from CardAccount service

            var recordTypeId = CaseRepository.GetRecordTypeId(request.RecordType, ObjectTypeEnum.Case);
            var personAccount = CaseRepository.GetPersonAccount(personAccountInput);//Get or Create person Account

            string ownerIdResult = null;
            if (ownerId != null)
            {
                ownerIdResult = await ownerId;
            }

            var recordTypeIdResult = await recordTypeId;
            var personAccountResult = await personAccount;
            if (!string.IsNullOrWhiteSpace(ownerIdResult))
            {
                request.OwnerId = ownerIdResult;
            }
            if (action == CaseActionEnum.Update)
            {
                if (!string.IsNullOrEmpty(request.CaseOwner) && string.IsNullOrEmpty(request.OwnerId))
                {
                    throw new NotFoundException("Salesforce case owner does not exist.",
                        new LogObject("GetDataFromExternalTask_UpdateExecuteAsync", null));
                }
            }
            if (!string.IsNullOrWhiteSpace(recordTypeIdResult))
            {
                request.RecordTypeId = recordTypeIdResult;
            }
            if (personAccountResult != null)
            {
                request.AccountId = personAccountResult.Id;
                request.ContactId = personAccountResult.PersonContactId;

            }
        }

        //private async Task GetCustomerInfo(PersonAccountInput personAccountInput)
        //{
        //    if (!string.IsNullOrEmpty(personAccountInput.AccountKey))
        //    {
        //        try
        //        {
        //            var cardAccountRequest = new CardAccountRequest()
        //            {
        //                CardAccountToken = personAccountInput.AccountKey
        //            };
        //            var customerInfo = await cardAccountService.GetCustomerByAccountKey(cardAccountRequest);

        //            personAccountInput.CustomerKey = customerInfo?.CustomerKey;
        //            personAccountInput.FirstName = customerInfo?.FirstName;
        //            personAccountInput.LastName = customerInfo?.LastName;
        //        }
        //        catch (ExternalErrorException ex)
        //        {
        //            Logger.Warn(new LogObject("GetCustomerByAccountKey", ex.ResponseText));
        //        }
        //    }
        //}

        public void Execute(CaseEx request, CaseActionEnum action)
        {
            throw new NotImplementedException();
        }

    }
}
