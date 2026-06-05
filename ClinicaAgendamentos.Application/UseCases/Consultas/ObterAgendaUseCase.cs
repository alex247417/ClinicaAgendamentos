using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;

namespace ClinicaAgendamentos.Application.UseCases.Consultas;

public class ObterAgendaUseCase
{
    private readonly IConsultaRepository _repository;

    public ObterAgendaUseCase(IConsultaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Consulta>> ExecutarAsync(int profissionalId, DateTime data)
    {
        return await _repository.ObterAgendaPorProfissionalAsync(profissionalId, data);
    }
}