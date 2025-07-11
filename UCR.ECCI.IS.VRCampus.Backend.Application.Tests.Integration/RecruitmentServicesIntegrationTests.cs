using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Integration;

/// <summary>
/// Provides integration tests for the recruitment services, ensuring that the service methods interact correctly with
/// the recruitment repository and produce expected results.
/// </summary>
/// <remarks>This class is designed to test the behavior of the <see cref="IRecruitmentServices"/> implementation
/// in scenarios involving recruitment-related operations, such as listing recruitments or adding new recruitment
/// information. It uses mocked dependencies to isolate the service logic and verify its correctness.  The tests in this
/// class validate the following: - Correct interaction between the service and the repository. - Expected outputs and
/// side effects of service methods. - Handling of typical scenarios, such as retrieving all recruitments or adding
/// recruitment data.  These tests are intended to ensure the reliability of the recruitment services in a controlled
/// environment.</remarks>
public class RecruitmentServicesIntegrationTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="IRecruitmentRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="IRecruitmentRepository"/>. It provides a controlled environment for testing without relying on the actual
    /// repository implementation.</remarks>
    private readonly Mock<IRecruitmentRepository> _recruitmentRepositoryMock;

    /// <summary>
    /// Represents the recruitment services used to perform operations related to recruitment.
    /// </summary>
    /// <remarks>This field is intended to store a reference to an implementation of the <see
    /// cref="IRecruitmentServices"/> interface. It is used internally by the class to interact with recruitment-related
    /// functionality.</remarks>
    private readonly IRecruitmentServices _recruitmentServices;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecruitmentServicesIntegrationTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary mocks and dependencies for testing the integration of 
    /// recruitment services. It creates a mock implementation of <see cref="IRecruitmentRepository"/>  and initializes
    /// a <see cref="RecruitmentServices"/> instance using the mock object.</remarks>
    public RecruitmentServicesIntegrationTests()
    {
        _recruitmentRepositoryMock = new Mock<IRecruitmentRepository>();
        _recruitmentServices = new RecruitmentServices(_recruitmentRepositoryMock.Object);
    }

    /// <summary>
    /// Tests that the <see cref="RecruitmentServices.ListRecruitmentsAsync"/> method returns all recruitments from the
    /// repository.
    /// </summary>
    /// <remarks>This test verifies that the service correctly retrieves all recruitment entries by mocking
    /// the repository and asserting the expected behavior, including the number of recruitments returned and the
    /// invocation of the repository method.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListRecruitmentsAsync_ShouldReturnAllRecruitments()
    {
        // Arrange
        var recruitments = new List<Recruitment>
        {
            new Recruitment(
                Description.Create("Recruit junior developers").Value!,
                Description.Create("Submit CV, Interview, Offer").Value!,
                Description.Create("Knowledge of C#, SQL").Value!,
                new List<Language> { new Language(LanguageVO.Create("English").Value!) }
            )
        };

        _recruitmentRepositoryMock
            .Setup(r => r.ListAllAsync())
            .ReturnsAsync(recruitments);

        // Act
        var result = await _recruitmentServices.ListRecruitmentsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _recruitmentRepositoryMock.Verify(r => r.ListAllAsync(), Times.Once);
    }

    /// <summary>
    /// Tests the <c>AddRecruitmentAsync</c> method to ensure it calls the repository to add recruitment information and
    /// returns a success result.
    /// </summary>
    /// <remarks>This test verifies that the <c>AddRecruitmentAsync</c> method correctly interacts with the
    /// repository and produces a successful result when valid inputs are provided. It also ensures the repository
    /// method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddRecruitmentAsync_ShouldCallRepositoryAndReturnSuccess()
    {
        // Arrange
        int careerId = 3;
        var recruitment = new Recruitment(
            Description.Create("Cloud computing recruitment").Value!,
            Description.Create("1. Apply 2. Technical test").Value!,
            Description.Create("Bachelor’s degree in CS").Value!,
            new List<Language> { new Language(LanguageVO.Create("Spanish").Value!) }
        );

        _recruitmentRepositoryMock
            .Setup(r => r.AddInformationAsync(careerId, recruitment))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _recruitmentServices.AddRecruitmentAsync(careerId, recruitment);

        // Assert
        Assert.True(result.IsSuccess);
        _recruitmentRepositoryMock.Verify(r => r.AddInformationAsync(careerId, recruitment), Times.Once);
    }
}
