namespace Infrastructure.Authentication
{
    public record TokenParam(string jwtKey, string audience, string issuer);
}
