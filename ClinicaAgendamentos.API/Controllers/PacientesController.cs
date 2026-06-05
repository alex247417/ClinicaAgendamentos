using ClinicaAgendamentos.Application.UseCases.Pacientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAgendamentos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PacientesController : ControllerBase
{
    private readonly CadastrarPacienteUseCase _cadastrar;
    private readonly ListarPacientesUseCase _listar;

    public PacientesController(
        CadastrarPacienteUseCase cadastrar,
        ListarPacientesUseCase listar)
    {
        _cadastrar = cadastrar;
        _listar = listar;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var pacientes = await _listar.ExecutarAsync();
        return Ok(pacientes);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] CadastrarPacienteRequest request)
    {
        await _cadastrar.ExecutarAsync(request.Nome, request.Cpf);
        return Created("", null);
    }
}

public record CadastrarPacienteRequest(string Nome, string Cpf);