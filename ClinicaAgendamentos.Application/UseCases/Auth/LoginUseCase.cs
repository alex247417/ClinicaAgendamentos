using ClinicaAgendamentos.Domain.Interfaces;
using ClinicaAgendamentos.Application.Interfaces;
using System;
using System.Threading.Tasks;

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
        
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash)) 
            throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

        return _tokenService.GerarToken(usuario);
    }
}