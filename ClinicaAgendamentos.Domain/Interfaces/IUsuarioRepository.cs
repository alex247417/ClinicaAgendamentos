using ClinicaAgendamentos.Domain.Entities;

namespace ClinicaAgendamentos.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmailAsync(string email);
    Task AdicionarAsync(Usuario usuario);
}