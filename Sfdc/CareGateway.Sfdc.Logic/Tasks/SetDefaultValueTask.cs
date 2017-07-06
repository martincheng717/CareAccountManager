using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CareGateway.Sfdc.Model;
using CareGateway.Sfdc.Model.Enum;
using Gdot.Care.Common.Exceptions;
using Gdot.Care.Common.Logging;

namespace CareGateway.Sfdc.Logic.Tasks
{
    public class SetDefaultValueTask:  ICaseTask<CaseEx>
    {
        public bool IsAsync { get; set; }
        public ICaseRepository CaseRepository { get; set; }

        public SetDefaultValueTask()
        {
            IsAsync = false;
        }
        public void Execute(CaseEx request, CaseActionEnum action)
        {
            var fieldConfig = CaseRepository.GetFields(request.RecordType, action);
            SetProperty(request, fieldConfig);
        }

        public Task ExecuteAsync(CaseEx request, CaseActionEnum action)
        {
            throw new NotImplementedException();
        }

        private void SetProperty(CaseEx request, FieldConfigInfo fieldConfig)
        {
            foreach (var field in fieldConfig.Fields)
            {
                try
                {
                    var property = CaseRepository.GetProperty(field.Name, request);
                    var currentValue = property.GetValue(request, null);
                    if (!string.IsNullOrWhiteSpace(field.DefaultValue) &&
                        (string.IsNullOrWhiteSpace(currentValue?.ToString())))
                    {
                        property.SetValue(request, field.DefaultValue);
                    }
                }
                catch (Exception ex)
                {
                    throw new GdErrorException($"Error setting default value for Field={field.Name}",
                        new LogObject("SetDefaultValueTask_SetProperty",
                            new Dictionary<string, object> {{"FieldName", field.Name}}), ex);
                }
            }
        }
    }
}
