using System.Threading.Tasks;
using CareGateway.Sfdc.Model.Enum;

namespace CareGateway.Sfdc.Logic.Tasks
{
    public interface ICaseTask<in TObject>
    {
        bool IsAsync { get; set; }
        void Execute(TObject request, CaseActionEnum action);
        Task ExecuteAsync(TObject request, CaseActionEnum action);
    }
}
