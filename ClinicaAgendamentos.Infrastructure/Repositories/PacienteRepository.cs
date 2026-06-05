using Dapper;
using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;
using ClinicaAgendamentos.Infrastructure.Data;

namespace ClinicaAgendamentos.Infrastructure.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly DbConnectionFactory _factory;

    public PacienteRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Paciente>> ObterTodosAsync()
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<Paciente>("SELECT * FROM Pacientes");
    }

    public async Task AdicionarAsync(Paciente paciente)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync(
            "INSERT INTO Pacientes (Nome, Cpf) VALUES (@Nome, @Cpf)",
            paciente);
    }
}