namespace PlanesRecetas.infraestructure.Extensions
{
    public class JwtOptions
    {
        public const string SectionName = "JwtOptions";

        public int Lifetime { get; set; }
        public string SecretKey { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateLifetime { get; set; }
    }
}