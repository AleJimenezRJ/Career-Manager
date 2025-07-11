using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Integration;

/// <summary>
/// Provides integration tests for the <see cref="ICareerServices"/> implementation.
/// </summary>
/// <remarks>This class is designed to test the interaction between the <see cref="ICareerServices"/> and its
/// underlying repository, <see cref="ICareerRepository"/>, ensuring that the service behaves as expected in various
/// scenarios. It uses mocked dependencies to isolate the service logic and verify its correctness.</remarks>
public class CareerServicesIntegrationTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="ICareerRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="ICareerRepository"/>. It is initialized as a readonly instance of <see cref="Mock{T}"/>.</remarks>
    private readonly Mock<ICareerRepository> _careerRepositoryMock;

    /// <summary>
    /// Represents the career services dependency used to manage career-related operations.
    /// </summary>
    /// <remarks>This field is intended to store an instance of a service implementing the <see
    /// cref="ICareerServices"/> interface. It is used internally to perform operations related to career services, such
    /// as job postings, career advice, or other related functionality.</remarks>
    private readonly ICareerServices _careerServices;

    /// <summary>
    /// Initializes a new instance of the <see cref="CareerServicesIntegrationTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary dependencies for testing the integration of  <see
    /// cref="CareerServices"/> with a mocked implementation of <see cref="ICareerRepository"/>.</remarks>
    public CareerServicesIntegrationTests()
    {
        _careerRepositoryMock = new Mock<ICareerRepository>();
        _careerServices = new CareerServices(_careerRepositoryMock.Object);
    }


    /// <summary>
    /// Tests the <see cref="ICareerServices.AddCareerAsync"/> method to ensure that it calls the repository to add a
    /// career and returns a success result.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ICareerRepository.AddCareerAsync"/> method is invoked
    /// exactly once with the specified career object and that the result of the operation indicates success.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddCareerAsync_ShouldCallRepositoryAndReturnSuccess()
    {
        // Arrange
        var career = new Career(
            EntityName.Create("Engineering").Value!,
            Description.Create("Engineering program").Value!,
            SemestersNumber.Create(10).Value!,
            Modality.Create("Virtual").Value!,
            DegreeTitle.Create("Bachelor").Value!,
            true
        );

        _careerRepositoryMock
            .Setup(r => r.AddCareerAsync(career))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _careerServices.AddCareerAsync(career);

        // Assert
        Assert.True(result.IsSuccess);
        _careerRepositoryMock.Verify(r => r.AddCareerAsync(career), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="CareerServices.ListCareersAsync"/> method retrieves a list of careers  from the
    /// repository and returns them correctly.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ICareerRepository.ListCareersAsync"/> method is called
    /// exactly once and that the returned list of careers matches the expected data.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListCareersAsync_ShouldReturnCareersFromRepository()
    {
        // Arrange
        var careers = new List<Career>
        {
            new Career(EntityName.Create("Medicine").Value!, null, null, null, null, false)
        };

        _careerRepositoryMock
            .Setup(r => r.ListCareersAsync())
            .ReturnsAsync(careers);

        // Act
        var result = await _careerServices.ListCareersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        _careerRepositoryMock.Verify(r => r.ListCareersAsync(), Times.Once);
    }

    /// <summary>
    /// Tests the <see cref="CareerServices.ListSpecificCareerAsync(string)"/> method to ensure it returns the expected
    /// career when queried by name.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="CareerServices.ListSpecificCareerAsync(string)"/>
    /// method correctly retrieves a career with the specified name from the repository and that the returned career
    /// matches the expected values. It also ensures that the repository method is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListSpecificCareerAsync_ShouldReturnSpecificCareer()
    {
        // Arrange
        var name = "Law";
        var expectedCareer = new Career(EntityName.Create(name).Value!, null, null, null, null, false);

        _careerRepositoryMock
            .Setup(r => r.ListSpecificCareerAsync(name))
            .ReturnsAsync(expectedCareer);

        // Act
        var result = await _careerServices.ListSpecificCareerAsync(name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result!.Name!.Name);
        _careerRepositoryMock.Verify(r => r.ListSpecificCareerAsync(name), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="CareerServices.CalculateScholarshipAsync(string)"/> method  returns a successful
    /// result when a valid career name exists in the repository.
    /// </summary>
    /// <remarks>This test verifies the behavior of the <see
    /// cref="CareerServices.CalculateScholarshipAsync(string)"/>  method when the specified career name is present in
    /// the repository. It ensures that the method  correctly interacts with the repository and returns a success
    /// result.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task CalculateScholarshipAsync_ShouldReturnSuccessWhenCareerExists()
    {
        // Arrange
        var careerName = "Science";

        _careerRepositoryMock
            .Setup(r => r.CalculateScholarshipAsync(careerName))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await _careerServices.CalculateScholarshipAsync(careerName);

        // Assert
        Assert.True(result.IsSuccess);
        _careerRepositoryMock.Verify(r => r.CalculateScholarshipAsync(careerName), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="CareerServices.SearchKeywordAsync(string)"/> method returns the expected search
    /// results when provided with a valid keyword.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="CareerServices.SearchKeywordAsync(string)"/> method
    /// correctly interacts with the repository to retrieve search results based on the provided keyword. It ensures
    /// that the method returns the expected results and that the repository is called exactly once.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task SearchKeywordAsync_ShouldReturnResults()
    {
        // Arrange
        var keyword = "biology";
        var expectedResults = new List<SearchResult>
        {
            new SearchResult { TableName = "Careers", ColumnName = "Name", CareerName = "Biology" }
        };

        _careerRepositoryMock
            .Setup(r => r.SearchKeywordAsync(keyword))
            .ReturnsAsync(expectedResults);

        // Act
        var results = await _careerServices.SearchKeywordAsync(keyword);

        // Assert
        Assert.Single(results);
        _careerRepositoryMock.Verify(r => r.SearchKeywordAsync(keyword), Times.Once);
    }
}
