using ClinicaAgendamentos.Application.UseCases.Profissionais;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAgendamentos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfissionaisController : ControllerBase
{
    private readonly CadastrarProfissionalUseCase _cadastrar;
    private readonly ListarProfissionaisUseCase _listar;

    public ProfissionaisController(
        CadastrarProfissionalUseCase cadastrar,
        ListarProfissionaisUseCase listar)
    {
        _cadastrar = cadastrar;
        _listar = listar;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var profissionais = await _listar.ExecutarAsync();
        return Ok(profissionais);
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar([FromBody] CadastrarProfissionalRequest request)
    {
        await _cadastrar.ExecutarAsync(request.Nome, request.Especialidade);
        return Created("", null);
    }
}

public record CadastrarProfissionalRequest(string Nome, string Especialidade);