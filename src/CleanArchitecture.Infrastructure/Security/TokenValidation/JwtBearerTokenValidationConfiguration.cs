using System.Text;

using CleanArchitecture.Infrastructure.Common.Settings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure.Security.TokenValidation;

public sealed class JwtBearerTokenValidationConfiguration : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AppSettings _appSettings;
    private readonly JwtSettings _jwtSettings;

    public JwtBearerTokenValidationConfiguration(IOptions<AppSettings> settingsOptions)
    {
        _appSettings = settingsOptions.Value;
        _jwtSettings = _appSettings.JwtSettings!;
    }

    public void Configure(string? name, JwtBearerOptions options) => Configure(options);

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
        };
    }
}
