using System.IdentityModel.Tokens.Jwt;

namespace Car_Catalogue.API.Model
{
    public class Jwt
    {
        public Jwt(string token, DateTime expiration)
        {
            Token = token;
            Expiration = expiration;
        }

        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
