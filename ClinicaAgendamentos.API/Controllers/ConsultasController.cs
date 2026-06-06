using ClinicaAgendamentos.Application.UseCases.Consultas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAgendamentos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ConsultasController : ControllerBase
{
    private readonly AgendarConsultaUseCase _agendarConsultaUseCase;
    private readonly ObterAgendaUseCase _obterAgendaUseCase;

    public ConsultasController(
        AgendarConsultaUseCase agendarConsultaUseCase,
        ObterAgendaUseCase obterAgendaUseCase)
    {
        _agendarConsultaUseCase = agendarConsultaUseCase;
        _obterAgendaUseCase = obterAgendaUseCase;
    }

    
    [HttpGet("agenda/{profissionalId}")]
    public async Task<IActionResult> ObterAgenda(int profissionalId, [FromQuery] DateTime data)
    {
        try
        {
            var consultas = await _obterAgendaUseCase.ExecutarAsync(profissionalId, data);
            return Ok(consultas);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = "Erro interno ao obter agenda." });
        }
    }
    
    [HttpPost("agendar")]
    public async Task<IActionResult> Agendar([FromBody] AgendarConsultaRequest request)
    {
        try
        {
            await _agendarConsultaUseCase.ExecutarAsync(
                request.PacienteId, 
                request.ProfissionalId, 
                request.DataHoraInicio);

            return Ok(new { mensagem = "Consulta agendada com sucesso!" });
        }
        catch (Exception ex)
        {
            // Regra de negócio violada
            return BadRequest(new { erro = ex.Message });
        }
    }
}


public class AgendarConsultaRequest
{
    public required int PacienteId { get; set; }
    public required int ProfissionalId { get; set; }
    public required DateTime DataHoraInicio { get; set; }
}