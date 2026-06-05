using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;

namespace ClinicaAgendamentos.Application.UseCases.Pacientes;

public class ListarPacientesUseCase
{
    private readonly IPacienteRepository _repository;

    public ListarPacientesUseCase(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Paciente>> ExecutarAsync()
    {
        return await _repository.ObterTodosAsync();
    }
}