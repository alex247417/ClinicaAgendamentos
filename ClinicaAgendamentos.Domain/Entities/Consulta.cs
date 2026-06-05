namespace ClinicaAgendamentos.Domain.Entities;

public class Consulta
{
    public int Id { get; private set; }
    public int PacienteId { get; private set; }
    public int ProfissionalId { get; private set; }
    public DateTime DataHoraInicio { get; private set; }
    public DateTime DataHoraFim { get; private set; }

    public Consulta(int pacienteId, int profissionalId, DateTime dataHoraInicio)
    {
        ValidarRegrasDeHorario(dataHoraInicio);

        PacienteId = pacienteId;
        ProfissionalId = profissionalId;
        DataHoraInicio = dataHoraInicio;
        DataHoraFim = dataHoraInicio.AddMinutes(30); // Regra: 30 minutos cravados
    }

    private void ValidarRegrasDeHorario(DateTime dataHora)
    {
        // Regra: Segunda a Sexta
        if (dataHora.DayOfWeek == DayOfWeek.Saturday || dataHora.DayOfWeek == DayOfWeek.Sunday)
            throw new Exception("Consultas apenas de segunda a sexta-feira.");

        // Regra: 08:00 às 18:00 (se a consulta dura 30 min, o último horário de início é 17:30)
        if (dataHora.TimeOfDay < new TimeSpan(8, 0, 0) || dataHora.TimeOfDay > new TimeSpan(17, 30, 0))
            throw new Exception("Consultas apenas entre 08:00 e 18:00.");
    }
}