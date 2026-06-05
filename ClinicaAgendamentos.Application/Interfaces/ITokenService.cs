using ClinicaAgendamentos.Domain.Entities;

namespace ClinicaAgendamentos.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(Usuario usuario);
}