using System.Text;
using DocHelper.Domain.Checker;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DocHelper.Domain.IdentityModel.Tokens
{
    public class TokenValidationFactory
    {
        private readonly TokenValidationOptions _options;

        public TokenValidationFactory(IOptions<TokenValidationOptions> options)
        {
            _options = options.Value;
            
            ArgumentCheck.NotNullOrWhiteSpace(_options.Issuer, nameof(_options.Issuer));
            ArgumentCheck.NotNullOrWhiteSpace(_options.Audience, nameof(_options.Audience));
            ArgumentCheck.NotNullOrWhiteSpace(_options.SigningKey, nameof(_options.SigningKey));
        }

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