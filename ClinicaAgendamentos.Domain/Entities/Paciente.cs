namespace ClinicaAgendamentos.Domain.Entities;

public class Paciente
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    
    protected Paciente() { }

    public Paciente(string nome, string cpf)
    {
        Nome = nome;
        Cpf = cpf;
    }
}