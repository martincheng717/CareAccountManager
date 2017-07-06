namespace CareGateway.Db.PartnerAuthentication.Model
{
    public class GetPartnerAuthenticationOutput
    {
        public string Login { get; set; }
        public string Domain { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string Password { get; set; }
    }
}
