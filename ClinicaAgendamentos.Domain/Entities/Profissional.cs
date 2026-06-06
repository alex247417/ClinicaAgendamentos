namespace ClinicaAgendamentos.Domain.Entities;

public class Profissional
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public string Especialidade { get; private set; } = null!;
    
    protected Profissional() { }

    public Profissional(string nome, string especialidade)
    {
        Nome = nome;
        Especialidade = especialidade;
    }
}