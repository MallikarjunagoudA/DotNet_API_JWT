namespace JWTDemo
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password);
       // string GenerateToken(string token);
    }
}
