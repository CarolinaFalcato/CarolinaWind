namespace QEnergy.Core.Domain.Configuration
{
    public class JwtAuthorization
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireInDays { get; set; }
    }
}
