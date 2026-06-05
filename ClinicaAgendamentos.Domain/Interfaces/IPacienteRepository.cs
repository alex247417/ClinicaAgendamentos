using ClinicaAgendamentos.Domain.Entities;

namespace ClinicaAgendamentos.Domain.Interfaces;

public interface IPacienteRepository
{
    Task<IEnumerable<Paciente>> ObterTodosAsync();
    Task AdicionarAsync(Paciente paciente);
}