using FluentAssertions;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Unit.Services.Implementations;

/// <summary>
/// Provides unit tests for the <see cref="RecruitmentServices"/> class.
/// Validates service behavior by mocking interactions with <see cref="IRecruitmentRepository"/>.
/// </summary>
public class RecruitmentServicesTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="IRecruitmentRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the recruitment
    /// repository. It provides a controlled environment for testing without relying on the actual
    /// implementation.</remarks>
    private readonly Mock<IRecruitmentRepository> _recruitmentRepositoryMock;

    /// <summary>
    /// Represents the recruitment services instance used for testing purposes.
    /// </summary>
    /// <remarks>This field is intended to hold a reference to the <see cref="RecruitmentServices"/> object
    /// being tested. It is marked as readonly to ensure its value cannot be modified after initialization.</remarks>
    private readonly RecruitmentServices _serviceUnderTest;

    /// <summary>
    /// Represents the recruitment process or system used within the application.
    /// </summary>
    /// <remarks>This field is read-only and is intended to store an instance of the <see cref="Recruitment"/>
    /// class. It is used internally to manage recruitment-related operations.</remarks>
    private readonly Recruitment _recruitment;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecruitmentServicesTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary mocks and test dependencies for testing the  <see
    /// cref="RecruitmentServices"/> class. It creates a strict mock of the  <see cref="IRecruitmentRepository"/>
    /// interface and initializes the service under test. Additionally, it prepares a sample <see cref="Recruitment"/>
    /// object with predefined  descriptions and languages for use in test scenarios.</remarks>
    public RecruitmentServicesTests()
    {
        _recruitmentRepositoryMock = new Mock<IRecruitmentRepository>(MockBehavior.Strict);
        _serviceUnderTest = new RecruitmentServices(_recruitmentRepositoryMock.Object);

        _recruitment = new Recruitment
        (
            Description.FromDatabase("Internship program"),
            Description.FromDatabase("Follow these steps to apply."),
            Description.FromDatabase("Must be enrolled in a related field."),
            new List<Language> { new Language(LanguageVO.FromDatabase("English")), new Language(LanguageVO.FromDatabase("Spanish")) }
        );
    }

    /// <summary>
    /// Tests that the <see cref="IRecruitmentService.ListRecruitmentsAsync"/> method returns a list of recruitments.
    /// </summary>
    /// <remarks>This test verifies that the service correctly retrieves all recruitments from the repository
    /// and returns them. It ensures that the repository's <see cref="IRecruitmentRepository.ListAllAsync"/> method is
    /// called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListRecruitmentsAsync_ReturnsRecruitmentList()
    {
        var recruitments = new List<Recruitment> { _recruitment };

        _recruitmentRepositoryMock
            .Setup(repo => repo.ListAllAsync())
            .ReturnsAsync(recruitments);

        var result = await _serviceUnderTest.ListRecruitmentsAsync();

        result.Should().ContainSingle().Which.Should().Be(_recruitment);
        _recruitmentRepositoryMock.Verify(repo => repo.ListAllAsync(), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="IRecruitmentService.AddRecruitmentAsync(int, Recruitment)"/> method  returns a
    /// successful result when the repository successfully adds the recruitment information.
    /// </summary>
    /// <remarks>This test verifies the behavior of the service when the repository returns a success result. 
    /// It ensures that the service correctly delegates the call to the repository and returns the  expected
    /// result.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddRecruitmentAsync_ReturnsSuccess_WhenRepositoryReturnsSuccess()
    {
        int careerId = 99;
        var expected = Result.Success();

        _recruitmentRepositoryMock
            .Setup(repo => repo.AddInformationAsync(careerId, _recruitment))
            .ReturnsAsync(expected);

        var result = await _serviceUnderTest.AddRecruitmentAsync(careerId, _recruitment);

        result.Should().Be(expected);
        _recruitmentRepositoryMock.Verify(repo => repo.AddInformationAsync(careerId, _recruitment), Times.Once);
    }
}
