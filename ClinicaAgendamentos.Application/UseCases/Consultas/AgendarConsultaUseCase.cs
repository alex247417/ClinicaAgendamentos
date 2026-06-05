using ClinicaAgendamentos.Domain.Entities;
using ClinicaAgendamentos.Domain.Interfaces;

namespace ClinicaAgendamentos.Application.UseCases.Consultas;

public class AgendarConsultaUseCase
{
    private readonly IConsultaRepository _consultaRepository;
    private readonly IPacienteRepository _pacienteRepository;
    private readonly IProfissionalRepository _profissionalRepository;

    public AgendarConsultaUseCase(
        IConsultaRepository consultaRepository,
        IPacienteRepository pacienteRepository,
        IProfissionalRepository profissionalRepository)
    {
        _consultaRepository = consultaRepository;
        _pacienteRepository = pacienteRepository;
        _profissionalRepository = profissionalRepository;
    }

    public async Task ExecutarAsync(int pacienteId, int profissionalId, DateTime dataHoraInicio)
    {
        // Regra 2: profissional já tem consulta nesse horário?
        var horarioOcupado = await _consultaRepository
            .ExisteConsultaNoHorarioAsync(profissionalId, dataHoraInicio);

        if (horarioOcupado)
            throw new Exception("Profissional já possui consulta nesse horário.");

        // Regra 1: paciente já tem consulta com esse profissional hoje?
        var pacienteJaAgendado = await _consultaRepository
            .PacienteJaPossuiConsultaNoDiaAsync(pacienteId, profissionalId, dataHoraInicio);

        if (pacienteJaAgendado)
            throw new Exception("Paciente já possui consulta com este profissional neste dia.");

        // Regras 3, 4 e 5: validadas dentro da própria entidade Consulta
        var consulta = new Consulta(pacienteId, profissionalId, dataHoraInicio);

        await _consultaRepository.AdicionarAsync(consulta);
    }
}