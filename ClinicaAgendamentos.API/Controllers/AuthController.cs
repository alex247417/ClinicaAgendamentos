using ClinicaAgendamentos.Application.UseCases.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAgendamentos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly LoginUseCase _loginUseCase;

    public AuthController(LoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _loginUseCase.ExecutarAsync(request.Email, request.Senha);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Senha { get; set; }
}