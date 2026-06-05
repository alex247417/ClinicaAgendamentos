using Xunit;
using ClinicaAgendamentos.Domain.Entities;

namespace ClinicaAgendamentos.Tests.Entities;

public class ConsultaTests
{
    [Fact]
    public void Deve_LancarExcecao_Quando_AgendamentoForNoFimDeSemana()
    {
        // Arrange
        var pacienteId = 1;
        var profissionalId = 1;
        var dataSabado = new DateTime(2026, 6, 6, 10, 0, 0); // 06/06/2026 cai num sábado

        // Act & Assert
        var exception = Assert.Throws<Exception>(() => new Consulta(pacienteId, profissionalId, dataSabado));
        Assert.Equal("Consultas apenas de segunda a sexta-feira.", exception.Message);
    }

    [Fact]
    public void Deve_CriarConsulta_Quando_HorarioForValido()
    {
        // Arrange
        var dataValida = new DateTime(2026, 6, 8, 14, 0, 0); // 08/06/2026 é Segunda, 14h

        // Act
        var consulta = new Consulta(1, 1, dataValida);

        // Assert
        Assert.Equal(dataValida.AddMinutes(30), consulta.DataHoraFim);
    }
}