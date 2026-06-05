using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;

namespace ClinicaAgendamentos.Application.UseCases.Pacientes;

public class CadastrarPacienteUseCase
{
    private readonly IPacienteRepository _repository;

    public CadastrarPacienteUseCase(IPacienteRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecutarAsync(string nome, string cpf)
    {
        var paciente = new Paciente(nome, cpf);
        await _repository.AdicionarAsync(paciente);
    }
}