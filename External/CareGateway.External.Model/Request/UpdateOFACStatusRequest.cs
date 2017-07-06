namespace CareGateway.External.Model.Request
{
    public class UpdateOFACStatusRequest
    {
        public string AccountIdentifier { get; set; }

        public bool IsOfacMatch { get; set; }

        public string CaseNumber { get; set; }
    }
}
