using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
namespace API.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        var tokenKey = configuration["TokenKey"] ?? throw new Exception("cannot find the token in appsettings");
        if (tokenKey.Length < 64) throw new Exception("token lenght is too short");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));

        //create claim
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier,user.UserName)
        };
        //create signature for descriptor

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds

        };

        //add token handler
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }

    public string CreateToken()
    {
        throw new NotImplementedException();
    }
}
