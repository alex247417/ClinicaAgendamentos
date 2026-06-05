using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;

namespace ClinicaAgendamentos.Application.UseCases.Profissionais;

public class ListarProfissionaisUseCase
{
    private readonly IProfissionalRepository _repository;

    public ListarProfissionaisUseCase(IProfissionalRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Profissional>> ExecutarAsync()
    {
        return await _repository.ObterTodosAsync();
    }
}