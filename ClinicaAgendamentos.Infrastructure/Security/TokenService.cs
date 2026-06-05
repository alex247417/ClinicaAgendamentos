using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClinicaAgendamentos.Application.Interfaces;
using ClinicaAgendamentos.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClinicaAgendamentos.Infrastructure.Security;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GerarToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        // Vai buscar a chave secreta que configuraremos no appsettings.json da API
        var secretKey = _configuration["Jwt:SecretKey"];
        if (string.IsNullOrEmpty(secretKey))
            throw new Exception("Chave JWT não configurada.");

        var key = Encoding.ASCII.GetBytes(secretKey);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(2), // Token válido por 2 horas
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}