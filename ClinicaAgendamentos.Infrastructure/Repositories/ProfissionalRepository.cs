using Dapper;
using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;
using ClinicaAgendamentos.Infrastructure.Data;

namespace ClinicaAgendamentos.Infrastructure.Repositories;

public class ProfissionalRepository : IProfissionalRepository
{
    private readonly DbConnectionFactory _factory;

    public ProfissionalRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Profissional>> ObterTodosAsync()
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<Profissional>("SELECT * FROM Profissionais");
    }

    public async Task AdicionarAsync(Profissional profissional)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync(
            "INSERT INTO Profissionais (Nome, Especialidade) VALUES (@Nome, @Especialidade)",
            profissional);
    }
}