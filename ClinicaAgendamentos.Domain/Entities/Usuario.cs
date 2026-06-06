namespace ClinicaAgendamentos.Domain.Entities;

public class Usuario
{
    public int Id { get; private set; }
    public string Email { get; private set; } = null!;
    public string SenhaHash { get; private set; } = null!;
    
    protected Usuario() { }

    public Usuario(string email, string senhaHash)
    {
        Email = email;
        SenhaHash = senhaHash;
    }
}