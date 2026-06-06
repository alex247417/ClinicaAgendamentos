namespace ClinicaAgendamentos.Domain.Entities;

public class Paciente
{
    public int Id { get; private set; }
    public string Nome { get; private set; } = null!;
    public string Cpf { get; private set; } = null!;
    
    protected Paciente() { }

    public Paciente(string nome, string cpf)
    {
        Nome = nome;
        Cpf = cpf;
    }
}