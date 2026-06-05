using ClinicaAgendamentos.Domain.Interfaces;
using ClinicaAgendamentos.Application.Interfaces;

namespace ClinicaAgendamentos.Application.UseCases.Auth;

public class LoginUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public LoginUseCase(IUsuarioRepository usuarioRepository, ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<string> ExecutarAsync(string email, string senha)
    {
        var usuario = await _usuarioRepository.ObterPorEmailAsync(email);
        
        // Simulação de validação de senha (em produção usaria BCrypt ou similar)
        if (usuario == null || usuario.SenhaHash != senha) 
            throw new Exception("Usuário ou senha inválidos.");

        return _tokenService.GerarToken(usuario);
    }
}