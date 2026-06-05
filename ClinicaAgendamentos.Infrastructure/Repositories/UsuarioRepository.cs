using Dapper;
using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;
using ClinicaAgendamentos.Infrastructure.Data;

namespace ClinicaAgendamentos.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DbConnectionFactory _factory;

    public UsuarioRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        using var conn = _factory.CreateConnection();
        // Aqui usamos o AS para mapear explicitamente a coluna Senha para a propriedade SenhaHash
        return await conn.QueryFirstOrDefaultAsync<Usuario>(
            "SELECT Id, Email, Senha AS SenhaHash FROM Usuarios WHERE Email = @Email",
            new { Email = email });
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync(
            "INSERT INTO Usuarios (Email, SenhaHash) VALUES (@Email, @SenhaHash)",
            usuario);
    }
}