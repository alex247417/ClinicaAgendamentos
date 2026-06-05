namespace ClinicaAgendamentos.Domain.Entities;

public class Profissional
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Especialidade { get; private set; }
    
    protected Profissional() { }

    public Profissional(string nome, string especialidade)
    {
        Nome = nome;
        Especialidade = especialidade;
    }
}