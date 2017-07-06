using System.Linq;
using CareGateway.Sfdc.Model;
using CareGateway.Sfdc.Model.Enum;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;
using Task = System.Threading.Tasks.Task;

namespace CareGateway.Sfdc.Logic.Tasks
{
    public class CaseValidationTask : ICaseTask<CaseEx>
    {
        public bool IsAsync { get; set; }
        public ICaseRepository CaseRepository { get; set; }
        public CaseValidationTask()
        {
            IsAsync = false;
        }
        public void Execute(CaseEx request, CaseActionEnum action)
        {
            if (request != null)
            {
                var fieldConfig = CaseRepository.GetFields(request.RecordType, action);
                if (action == CaseActionEnum.Create)
                {
                    if (!AllowToCreateNewCase())
                    {
                        throw new BadRequestException(
                            "Not allowed to create a new case because the current case is still in pending",
                            new LogObject("CaseValidationTask_ExecuteAsync", null));
                    }
                }
                Validation(request, fieldConfig);
            }
        }

        public Task ExecuteAsync( CaseEx request, CaseActionEnum action)
        {
            throw new GdErrorException("ExecuteAsync not implemented");
        }


        /// <summary>
        /// Check if we're allowing to create case if pending case existed
        /// </summary>
        /// <returns></returns>
        private static bool AllowToCreateNewCase()
        {
            //TODO: need to implement this
            return true;
        }
        private void Validation(CaseEx request, FieldConfigInfo fieldConfig)
        {
            if (string.IsNullOrWhiteSpace(request.RecordType))
            {
                throw new BadRequestException("RecordType is missing from request or empty",
                    new LogObject("CaseValidationTask_ExecuteAsync", null));
            }


            var properties = CaseRepository.GetProperties(request);
            //check to be sure that they pass data to un-used fields for a specific case
            var notUseFields = properties.Where(p => fieldConfig.Fields.All(f => f.Name != p.Name));
            CaseRepository.ValidateNotAllowedFields(notUseFields, request);
            foreach (var field in fieldConfig.Fields)
            {
                CaseRepository.ValidateField(request, field);
            }
        }

    }
}

