using ClinicaAgendamentos.Domain.Entities;

namespace ClinicaAgendamentos.Domain.Interfaces;

public interface IConsultaRepository
{
    Task AdicionarAsync(Consulta consulta);
    Task<IEnumerable<Consulta>> ObterAgendaPorProfissionalAsync(int profissionalId, DateTime data);
    Task<bool> ExisteConsultaNoHorarioAsync(int profissionalId, DateTime dataHoraInicio);
    Task<bool> PacienteJaPossuiConsultaNoDiaAsync(int pacienteId, int profissionalId, DateTime data);
}