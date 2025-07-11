using FluentAssertions;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Unit.Services.Implementations;

/// <summary>
/// Provides unit tests for the <see cref="EnterpriseServices"/> class, ensuring its methods behave as expected.
/// </summary>
/// <remarks>This test class uses the <see cref="Mock{T}"/> framework to mock dependencies and verify interactions
/// with the <see cref="IEnterpriseRepository"/> interface. It includes tests for listing enterprises and adding
/// enterprise information, validating both success scenarios and expected behaviors.</remarks>
public class EnterpriseServicesTests
{
    /// <summary>
    /// A mock implementation of the <see cref="IEnterpriseRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="IEnterpriseRepository"/>. It provides a controlled environment for testing without relying on the actual
    /// repository implementation.</remarks>
    private readonly Mock<IEnterpriseRepository> _enterpriseRepositoryMock;

    /// <summary>
    /// Represents the enterprise services instance used for internal operations.
    /// </summary>
    /// <remarks>This field is read-only and intended for internal use within the class. It provides access to
    /// enterprise-level functionality required by the class.</remarks>
    private readonly EnterpriseServices _serviceUnderTest;

    /// <summary>
    /// Represents the enterprise associated with the current context.
    /// </summary>
    /// <remarks>This field is read-only and is intended to store the enterprise instance for internal
    /// use.</remarks>
    private readonly Enterprise _enterprise;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnterpriseServicesTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary dependencies for testing the <see
    /// cref="EnterpriseServices"/> class. It creates a strict mock of <see cref="IEnterpriseRepository"/> and
    /// initializes the service under test. Additionally, it prepares a sample <see cref="Enterprise"/> instance for use
    /// in test scenarios.</remarks>
    public EnterpriseServicesTests()
    {
        _enterpriseRepositoryMock = new Mock<IEnterpriseRepository>(MockBehavior.Strict);
        _serviceUnderTest = new EnterpriseServices(_enterpriseRepositoryMock.Object);

        _enterprise = new Enterprise(
            Description.FromDatabase("An international tech company."),
            EntityName.FromDatabase("Tech Solutions"),
            Country.FromDatabase("Costa Rica")
        );
    }

    /// <summary>
    /// Tests that the <see cref="IEnterpriseService.ListEnterpriseAsync"/> method returns a list containing the
    /// expected enterprises.
    /// </summary>
    /// <remarks>This test verifies that the service correctly retrieves all enterprises from the repository
    /// and returns them. It ensures that the repository's <see cref="IEnterpriseRepository.ListAllAsync"/> method is
    /// called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListEnterpriseAsync_ReturnsEnterpriseList()
    {
        var enterprises = new List<Enterprise> { _enterprise };

        _enterpriseRepositoryMock
            .Setup(repo => repo.ListAllAsync())
            .ReturnsAsync(enterprises);

        var result = await _serviceUnderTest.ListEnterpriseAsync();

        result.Should().ContainSingle().Which.Should().Be(_enterprise);
        _enterpriseRepositoryMock.Verify(repo => repo.ListAllAsync(), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="IEnterpriseService.AddEnterpriseAsync(int, Enterprise)"/> method  returns a successful
    /// result when the repository successfully adds the enterprise information.
    /// </summary>
    /// <remarks>This test verifies the behavior of the <see cref="IEnterpriseService.AddEnterpriseAsync(int,
    /// Enterprise)"/>  method when the underlying repository returns a successful result. It ensures that the service
    /// correctly  propagates the success result and calls the repository method exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddEnterpriseAsync_ReturnsSuccess_WhenRepositoryReturnsSuccess()
    {
        int careerId = 1;
        var expected = Result.Success();

        _enterpriseRepositoryMock
            .Setup(repo => repo.AddInformationAsync(careerId, _enterprise))
            .ReturnsAsync(expected);

        var result = await _serviceUnderTest.AddEnterpriseAsync(careerId, _enterprise);

        result.Should().Be(expected);
        _enterpriseRepositoryMock.Verify(repo => repo.AddInformationAsync(careerId, _enterprise), Times.Once);
    }
}
