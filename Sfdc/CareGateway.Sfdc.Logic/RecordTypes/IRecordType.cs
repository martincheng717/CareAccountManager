using CareGateway.Sfdc.Model;

namespace CareGateway.Sfdc.Logic.RecordTypes
{
    public interface IRecordType
    {
        void Execute(CaseEx request);
    }
}