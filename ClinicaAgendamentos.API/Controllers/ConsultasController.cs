using ClinicaAgendamentos.Application.UseCases.Consultas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAgendamentos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Bloqueia o acesso para quem não tem o Token JWT
public class ConsultasController : ControllerBase
{
    private readonly AgendarConsultaUseCase _agendarConsultaUseCase;

    public ConsultasController(AgendarConsultaUseCase agendarConsultaUseCase)
    {
        _agendarConsultaUseCase = agendarConsultaUseCase;
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

// Crie este DTO no mesmo arquivo ou na camada Application -> DTOs
public class AgendarConsultaRequest
{
    public int PacienteId { get; set; }
    public int ProfissionalId { get; set; }
    public DateTime DataHoraInicio { get; set; }
}