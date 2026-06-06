namespace ClinicaAgendamentos.Domain.Entities;

public class Consulta
{
    public int Id { get; private set; }
    public int PacienteId { get; private set; }
    public int ProfissionalId { get; private set; }
    public DateTime DataHoraInicio { get; private set; }
    public DateTime DataHoraFim { get; private set; }
    
    protected Consulta() { }

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
        // Regra: Não permitir data no passado
        if (dataHora < DateTime.Now)
            throw new Exception("Não é possível agendar consultas em datas anteriores.");

        // Regra: Segunda a Sexta
        if (dataHora.DayOfWeek == DayOfWeek.Saturday || dataHora.DayOfWeek == DayOfWeek.Sunday)
            throw new Exception("Consultas apenas de segunda a sexta-feira.");

        // Regra: 08:00 às 18:00 (último horário de início é 17:30)
        if (dataHora.TimeOfDay < new TimeSpan(8, 0, 0) || dataHora.TimeOfDay > new TimeSpan(17, 30, 0))
            throw new Exception("Consultas apenas entre 08:00 e 18:00.");

        // Regra: Intervalos de 30 minutos (impede agendar 14:15, por exemplo)
        if (dataHora.Minute != 0 && dataHora.Minute != 30)
            throw new Exception("Os agendamentos devem ser em intervalos de 30 minutos (ex: 14:00, 14:30).");
    }
}