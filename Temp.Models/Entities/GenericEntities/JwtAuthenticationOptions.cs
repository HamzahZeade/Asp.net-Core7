namespace Temp.Models.Entities.GenericEntities
{
    public class JwtAuthenticationOptions
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string IssuerSigningKey { get; set; }
    }

}
