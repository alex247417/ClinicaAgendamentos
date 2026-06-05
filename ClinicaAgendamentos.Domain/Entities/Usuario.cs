namespace ClinicaAgendamentos.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string SenhaHash { get; private set; }
    
    // Construtor vazio para o Dapper
    protected Usuario() { }

    public Usuario(string email, string senhaHash)
    {
        Email = email;
        SenhaHash = senhaHash;
    }
}