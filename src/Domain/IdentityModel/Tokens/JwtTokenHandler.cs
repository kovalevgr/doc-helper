using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DocHelper.Domain.Checker;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DocHelper.Domain.IdentityModel.Tokens
{
    public class JwtTokenHandler
    {
        private readonly TokenOptions _options;

        public JwtTokenHandler(IOptions<TokenOptions> options)
        {
            _options = options.Value;
            
            ArgumentCheck.NotNullOrWhiteSpace(_options.Issuer, nameof(_options.Issuer));
            ArgumentCheck.NotNullOrWhiteSpace(_options.Audience, nameof(_options.Audience));
            ArgumentCheck.NotNullOrWhiteSpace(_options.SigningKey, nameof(_options.SigningKey));
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

                TokenValidationParameters parameters = CreateTokenValidationParameters();

                return tokenHandler.ValidateToken(token, parameters, out _);
            }
            catch
            {
                return GetDefaultClaimsPrincipal();
            }
        }
        
        public ClaimsPrincipal GetDefaultClaimsPrincipal() => new(new ClaimsIdentity());
        
        public TokenValidationParameters CreateTokenValidationParameters() =>
            new()
            {
                ValidateIssuer = _options.ValidateIssuer,
                ValidateAudience = _options.ValidateAudience,
                ValidateLifetime = _options.ValidateLifetime,
                ValidateIssuerSigningKey = _options.ValidateIssuerSigningKey,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey))
            }
        ;
    }
}