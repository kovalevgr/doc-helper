using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace DocHelper.Domain.IdentityModel.Tokens
{
    public class JwtTokenHandler
    {
        private readonly TokenValidationFactory _tokenValidationFactory;

        public JwtTokenHandler(TokenValidationFactory tokenValidationFactory)
        {
            _tokenValidationFactory = tokenValidationFactory;
        }
        
        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken) tokenHandler.ReadToken(token);
                if (jwtToken is null)
                {
                    return GetDefaultClaimsPrincipal();
                }

                TokenValidationParameters parameters = _tokenValidationFactory.CreateTokenValidationParameters();

                return tokenHandler.ValidateToken(token, parameters, out _);
            }
            catch
            {
                return GetDefaultClaimsPrincipal();
            }
        }
        
        public ClaimsPrincipal GetDefaultClaimsPrincipal() => new(new ClaimsIdentity());
    }
}