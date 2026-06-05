using Dapper;
using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;
using ClinicaAgendamentos.Infrastructure.Data;

namespace ClinicaAgendamentos.Infrastructure.Repositories;

public class ConsultaRepository : IConsultaRepository
{
    private readonly DbConnectionFactory _factory;

    public ConsultaRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task AdicionarAsync(Consulta consulta)
    {
        using var conn = _factory.CreateConnection();
        await conn.ExecuteAsync(
            @"INSERT INTO Consultas (PacienteId, ProfissionalId, DataHoraInicio, DataHoraFim) 
              VALUES (@PacienteId, @ProfissionalId, @DataHoraInicio, @DataHoraFim)",
            consulta);
    }

    public async Task<IEnumerable<Consulta>> ObterAgendaPorProfissionalAsync(int profissionalId, DateTime data)
    {
        using var conn = _factory.CreateConnection();
        return await conn.QueryAsync<Consulta>(
            @"SELECT * FROM Consultas 
              WHERE ProfissionalId = @ProfissionalId 
              AND DATE(DataHoraInicio) = DATE(@Data)",
            new { ProfissionalId = profissionalId, Data = data });
    }

    public async Task<bool> ExisteConsultaNoHorarioAsync(int profissionalId, DateTime dataHoraInicio)
    {
        using var conn = _factory.CreateConnection();
        var resultado = await conn.QueryFirstOrDefaultAsync<int>(
            @"SELECT COUNT(1) FROM Consultas 
              WHERE ProfissionalId = @ProfissionalId 
              AND DataHoraInicio = @DataHoraInicio",
            new { ProfissionalId = profissionalId, DataHoraInicio = dataHoraInicio });
        return resultado > 0;
    }

    public async Task<bool> PacienteJaPossuiConsultaNoDiaAsync(int pacienteId, int profissionalId, DateTime data)
    {
        using var conn = _factory.CreateConnection();
        var resultado = await conn.QueryFirstOrDefaultAsync<int>(
            @"SELECT COUNT(1) FROM Consultas 
              WHERE PacienteId = @PacienteId 
              AND ProfissionalId = @ProfissionalId 
              AND DATE(DataHoraInicio) = DATE(@Data)",
            new { PacienteId = pacienteId, ProfissionalId = profissionalId, Data = data });
        return resultado > 0;
    }
}