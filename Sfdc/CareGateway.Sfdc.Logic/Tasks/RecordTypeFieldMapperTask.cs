using System;
using System.IO;
using System.Threading.Tasks;
using CareGateway.Sfdc.Logic.RecordTypes;
using CareGateway.Sfdc.Model;
using CareGateway.Sfdc.Model.Enum;
using Gdot.Care.Common.Utilities;

namespace CareGateway.Sfdc.Logic.Tasks
{
    public class RecordTypeFieldMapperTask : ICaseTask<CaseEx>
    {
        private readonly Func<string, IRecordType> _createRecordTypeFunc;
        public ICaseRepository CaseRepository { get; set; }
        public bool IsAsync { get; set; }

        public RecordTypeFieldMapperTask(Func<string, IRecordType> func)
        {
            IsAsync = false;
            _createRecordTypeFunc = func;
        }
        public void Execute(CaseEx request, CaseActionEnum action)
        {
            var fieldConfig = CaseRepository.GetFields(request.RecordType, action);
            var sfdcSetting = SfdcSetting.Setting(request.RecordType);
            if (sfdcSetting != null)
            {
                if (string.IsNullOrWhiteSpace(fieldConfig.AssemplyName))
                {
                    //make assumption that AssemplyName is the same as the current project's AssemplyName
                    fieldConfig.AssemplyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                }
                if (string.IsNullOrWhiteSpace(fieldConfig.ClassName))
                {
                    //make assummption that a class a created under Gdot.Partner.Care.SFDC.Logic.RecordTypes namespace + recordtype
                    fieldConfig.ClassName = action == CaseActionEnum.Create
                        ? $"{fieldConfig.AssemplyName}.RecordTypes.{Path.GetFileNameWithoutExtension(sfdcSetting.CaseSettingPath)}"
                        : $"{fieldConfig.AssemplyName}.RecordTypes.{Path.GetFileNameWithoutExtension(sfdcSetting.UpdateCaseSettingPath)}";
                }
                var recordTypeObj = _createRecordTypeFunc(request.RecordType);
                recordTypeObj?.Execute(request);
            }
        }

        public Task ExecuteAsync(CaseEx request, CaseActionEnum action)
        {
            throw new NotImplementedException();
        }
    }
}
