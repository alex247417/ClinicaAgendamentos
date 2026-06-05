using ClinicaAgendamentos.Domain.Entities;

namespace ClinicaAgendamentos.Domain.Interfaces;

public interface IProfissionalRepository
{
    Task<IEnumerable<Profissional>> ObterTodosAsync();
    Task AdicionarAsync(Profissional profissional);
}