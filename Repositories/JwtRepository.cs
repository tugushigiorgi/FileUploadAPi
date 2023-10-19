using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FileUploaderApi.Data_Transfer_Objects;
using FileUploaderApi.Database.Models;
using FileUploaderApi.Services;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FileUploaderApi.Repositories;

public class JwtRepository:IJwtService
{

    public IConfiguration _configuration;

    public JwtRepository(IConfiguration config)
    {
        _configuration = config;
    }


    public string CreateToken(string userid,string Email)
    {
        var  expiration =DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTE"]));
                
        Claim[] claims = new Claim[] {
             new Claim(JwtRegisteredClaimNames.Sub, userid),  
            new Claim(JwtRegisteredClaimNames.UniqueName, Email),  
        };
        
        var  securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!) );

        var  signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiration,
            SigningCredentials = signingCredentials,
            Issuer = _configuration["Jwt:ISSUER"],
            Audience = _configuration["Jwt:AUDIANCE"],  
        };
        var  tokenHandler = new JwtSecurityTokenHandler();
        var createtoken = tokenHandler.CreateToken(descriptor);
        string token =tokenHandler.WriteToken(createtoken);
        return token;
    }

    public bool CheckExpiredToken(string accesToken)
    {
        var issuer = _configuration["Jwt:ISSUER"];
        var audiance = _configuration["Jwt:AUDIANCE"];


        var validator = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
            ValidAudience = audiance,
            ValidIssuer = issuer


        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {

            var principal= tokenHandler.ValidateToken(accesToken, validator, out SecurityToken sectoken);

            return sectoken != null;

        }
        catch (SecurityTokenException)
        {
            return false;

        }
        
        
        
        
        
        
        
        
        
    }
}


