using Xunit;
using Moq;
using ClinicaAgendamentos.Application.UseCases.Consultas;
using ClinicaAgendamentos.Domain.Interfaces;

namespace ClinicaAgendamentos.Tests.UseCases;

public class AgendarConsultaUseCaseTests
{
    [Fact]
    public async Task Deve_LancarExcecao_Quando_PacienteJaTiverConsultaNoDia()
    {
        // Arrange
        var consultaRepoMock = new Mock<IConsultaRepository>();
        var pacienteRepoMock = new Mock<IPacienteRepository>();
        var profissionalRepoMock = new Mock<IProfissionalRepository>();

        // Simulando que o banco de dados respondeu que o paciente JÁ TEM consulta
        consultaRepoMock.Setup(repo => repo.PacienteJaPossuiConsultaNoDiaAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
            .ReturnsAsync(true);

        var useCase = new AgendarConsultaUseCase(consultaRepoMock.Object, pacienteRepoMock.Object, profissionalRepoMock.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            useCase.ExecutarAsync(1, 1, new DateTime(2026, 6, 8, 10, 0, 0)));

        Assert.Equal("Paciente já possui consulta com este profissional neste dia.", exception.Message);
    }
}