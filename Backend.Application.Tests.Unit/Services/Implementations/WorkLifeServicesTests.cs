using FluentAssertions;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Unit.Services.Implementations;

/// <summary>
/// Provides unit tests for the <see cref="WorkLifeServices"/> class.
/// Validates logic by mocking interactions with <see cref="IWorkLifeRepository"/>.
/// </summary>
public class WorkLifeServicesTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="IWorkLifeRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="IWorkLifeRepository"/>. It is initialized as a readonly instance of <see cref="Mock{T}"/>.</remarks>
    private readonly Mock<IWorkLifeRepository> _workLifeRepositoryMock;

    /// <summary>
    /// Represents the service being tested in the current context.
    /// </summary>
    /// <remarks>This field is intended for internal use within the test class to hold a reference  to the
    /// instance of <see cref="WorkLifeServices"/> being tested.</remarks>
    private readonly WorkLifeServices _serviceUnderTest;

    /// <summary>
    /// Represents the work-life balance configuration or state associated with the current context.
    /// </summary>
    /// <remarks>This field is read-only and is intended to store an instance of the <see cref="WorkLife"/>
    /// class. It is used internally to manage work-life balance-related operations or data.</remarks>
    private readonly WorkLife _workLife;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkLifeServicesTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary mocks and test dependencies for testing the <see
    /// cref="WorkLifeServices"/> class. It creates a strict mock of <see cref="IWorkLifeRepository"/> and initializes
    /// the service under test. Additionally, it prepares a sample <see cref="WorkLife"/> object for use in test
    /// scenarios.</remarks>
    public WorkLifeServicesTests()
    {
        _workLifeRepositoryMock = new Mock<IWorkLifeRepository>(MockBehavior.Strict);
        _serviceUnderTest = new WorkLifeServices(_workLifeRepositoryMock.Object);

        _workLife = new WorkLife
        (
            Description.FromDatabase("Flexible work schedules"),
            Workers.FromDatabase(45),
            Workers.FromDatabase(15)
        );
    }

    /// <summary>
    /// Tests that the <see cref="IWorkLifeService.ListWorkLifeAsync"/> method returns a list of <see cref="WorkLife"/>
    /// objects.
    /// </summary>
    /// <remarks>This test verifies that the service correctly retrieves all <see cref="WorkLife"/> entities
    /// from the repository and returns them as part of the result. It also ensures that the repository's <see
    /// cref="IWorkLifeRepository.ListAllAsync"/>  method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListWorkLifeAsync_ReturnsWorkLifeList()
    {
        var workLifes = new List<WorkLife> { _workLife };

        _workLifeRepositoryMock
            .Setup(repo => repo.ListAllAsync())
            .ReturnsAsync(workLifes);

        var result = await _serviceUnderTest.ListWorkLifeAsync();

        result.Should().ContainSingle().Which.Should().Be(_workLife);
        _workLifeRepositoryMock.Verify(repo => repo.ListAllAsync(), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="IWorkLifeService.AddWorkLifeAsync(int, WorkLife)"/> method  returns a successful
    /// result when the repository successfully adds the work-life information.
    /// </summary>
    /// <remarks>This test verifies the interaction between the service and the repository, ensuring that  the
    /// repository's <see cref="IWorkLifeRepository.AddInformationAsync(int, WorkLife)"/> method  is called exactly once
    /// with the expected parameters.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddWorkLifeAsync_ReturnsSuccess_WhenRepositoryReturnsSuccess()
    {
        int careerId = 101;
        var expected = Result.Success();

        _workLifeRepositoryMock
            .Setup(repo => repo.AddInformationAsync(careerId, _workLife))
            .ReturnsAsync(expected);

        var result = await _serviceUnderTest.AddWorkLifeAsync(careerId, _workLife);

        result.Should().Be(expected);
        _workLifeRepositoryMock.Verify(repo => repo.AddInformationAsync(careerId, _workLife), Times.Once);
    }
}
