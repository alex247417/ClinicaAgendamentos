using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;

namespace ClinicaAgendamentos.Application.UseCases.Profissionais;

public class CadastrarProfissionalUseCase
{
    private readonly IProfissionalRepository _repository;

    public CadastrarProfissionalUseCase(IProfissionalRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecutarAsync(string nome, string especialidade)
    {
        var profissional = new Profissional(nome, especialidade);
        await _repository.AdicionarAsync(profissional);
    }
}