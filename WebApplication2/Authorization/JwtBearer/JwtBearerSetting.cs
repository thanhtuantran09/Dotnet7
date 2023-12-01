namespace WebApplication2.Authorization.JwtBearer
{
    public class JwtBearerSetting
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }

        public string? SigninKey { get; set; }

    }
}
