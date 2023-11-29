using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiProject.Models;
using Microsoft.IdentityModel.Tokens;
using WebApiProject.Data;

namespace WebApiProject.Services;

public interface IJWTService
{
    Token Authenticate(User user); 
}
public class JWTService : IJWTService
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration iconfiguration;
    public JWTService(IConfiguration iconfiguration, AppDbContext dbContext)
    {
        this.iconfiguration = iconfiguration;
        _dbContext = dbContext;
    }
    public Token Authenticate(User user)
    {
        if (!_dbContext.Users.Any(x => x.Pin == user.Pin && x.Password == user.Password)) {
            return null;
        }
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Fio)                    
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token { AccessToken = tokenHandler.WriteToken(token) };

    }
}