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
        var consultas = await _obterAgendaUseCase.ExecutarAsync(profissionalId, data);
        return Ok(consultas);
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
            // Retorna um erro amigável se bater em alguma regra de negócio (DDD)
            return BadRequest(new { erro = ex.Message });
        }
    }
}


public class AgendarConsultaRequest
{
    public int PacienteId { get; set; }
    public int ProfissionalId { get; set; }
    public DateTime DataHoraInicio { get; set; }
}