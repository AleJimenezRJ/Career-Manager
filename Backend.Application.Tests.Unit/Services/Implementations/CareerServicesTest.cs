using FluentAssertions;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Unit.Services.Implementations;

/// <summary>
/// Provides unit tests for the <see cref="CareerServices"/> class.
/// Validates the service layer logic by mocking interactions with <see cref="ICareerRepository"/>.
/// </summary>
public class CareerServicesTests
{
    /// <summary>
    /// Represents a mock implementation of the <see cref="ICareerRepository"/> interface.
    /// </summary>
    /// <remarks>This field is intended for use in unit tests to simulate the behavior of the <see
    /// cref="ICareerRepository"/>. It is initialized as a readonly instance of <see cref="Mock{T}"/>.</remarks>
    private readonly Mock<ICareerRepository> _careerRepositoryMock;

    /// <summary>
    /// Represents the career services instance used for testing purposes.
    /// </summary>
    /// <remarks>This field is intended to hold a reference to the <see cref="CareerServices"/> object being
    /// tested. It is read-only and cannot be modified after initialization.</remarks>
    private readonly CareerServices _serviceUnderTest;

    /// <summary>
    /// Represents the career associated with this instance.
    /// </summary>
    /// <remarks>This field is read-only and is intended for internal use within the class. It stores the
    /// career information that may be used by other members of the class.</remarks>
    private readonly Career _career;

    /// <summary>
    /// Initializes a new instance of the <see cref="CareerServicesTests"/> class.
    /// </summary>
    /// <remarks>This constructor sets up the necessary mocks and test objects for unit testing the <see
    /// cref="CareerServices"/> class. It creates a strict mock of <see cref="ICareerRepository"/> and initializes a
    /// <see cref="CareerServices"/> instance using the mock. Additionally, it prepares a sample <see cref="Career"/>
    /// object for use in test scenarios.</remarks>
    public CareerServicesTests()
    {
        _careerRepositoryMock = new Mock<ICareerRepository>(MockBehavior.Strict);
        _serviceUnderTest = new CareerServices(_careerRepositoryMock.Object);

        _career = new Career(
            EntityName.FromDatabase("Computer Science"),
            Description.FromDatabase("A technical career in computing."),
            SemestersNumber.FromDatabase(10),
            Modality.FromDatabase("Virtual"),
            DegreeTitle.FromDatabase("Bachelor"),
            true
        );
    }

    /// <summary>
    /// Tests that the <see cref="ICareerService.AddCareerAsync"/> method returns a success result when the repository
    /// successfully adds a career.
    /// </summary>
    /// <remarks>This test verifies the interaction between the service and the repository, ensuring that the
    /// repository's success result is correctly propagated by the service.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task AddCareerAsync_ReturnsSuccess_WhenRepositoryReturnsSuccess()
    {
        var successResult = Result.Success();

        _careerRepositoryMock
            .Setup(repo => repo.AddCareerAsync(_career))
            .ReturnsAsync(successResult);

        var result = await _serviceUnderTest.AddCareerAsync(_career);

        result.Should().Be(successResult);
        _careerRepositoryMock.Verify(repo => repo.AddCareerAsync(_career), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="ICareerService.ListCareersAsync"/> method returns a list of careers.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ICareerService.ListCareersAsync"/> method correctly
    /// retrieves  a list of careers from the repository and returns it. It ensures that the method calls the repository
    /// exactly once and that the returned list contains the expected career.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListCareersAsync_ReturnsCareersList()
    {
        var careers = new List<Career> { _career };

        _careerRepositoryMock
            .Setup(repo => repo.ListCareersAsync())
            .ReturnsAsync(careers);

        var result = await _serviceUnderTest.ListCareersAsync();

        result.Should().ContainSingle().Which.Should().Be(_career);
        _careerRepositoryMock.Verify(repo => repo.ListCareersAsync(), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="ICareerService.ListSpecificCareerAsync(string)"/> method  returns the expected career
    /// when a career with the specified name is found.
    /// </summary>
    /// <remarks>This test verifies that the service correctly retrieves a career by name and ensures  the
    /// repository method is called exactly once with the specified name.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListSpecificCareerAsync_ReturnsCareer_WhenFound()
    {
        var name = "Computer Science";

        _careerRepositoryMock
            .Setup(repo => repo.ListSpecificCareerAsync(name))
            .ReturnsAsync(_career);

        var result = await _serviceUnderTest.ListSpecificCareerAsync(name);

        result.Should().Be(_career);
        _careerRepositoryMock.Verify(repo => repo.ListSpecificCareerAsync(name), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="ICareerRepository.ListSpecificCareerAsync(string)"/> method  returns <see
    /// langword="null"/> when the specified career name does not exist.
    /// </summary>
    /// <remarks>This test verifies the behavior of the <see
    /// cref="ICareerRepository.ListSpecificCareerAsync(string)"/>  method when the repository does not contain a career
    /// matching the provided name. It ensures that the service correctly handles the case where no matching career is
    /// found.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task ListSpecificCareerAsync_ReturnsNull_WhenNotFound()
    {
        var name = "Unknown Career";

        _careerRepositoryMock
            .Setup(repo => repo.ListSpecificCareerAsync(name))
            .ReturnsAsync((Career?)null);

        var result = await _serviceUnderTest.ListSpecificCareerAsync(name);

        result.Should().BeNull();
        _careerRepositoryMock.Verify(repo => repo.ListSpecificCareerAsync(name), Times.Once);
    }

    /// <summary>
    /// Tests that the <see cref="ICareerRepository.CalculateScholarshipAsync(string)"/> method  returns the expected
    /// result when invoked with a valid career name.
    /// </summary>
    /// <remarks>This test verifies the behavior of the <see
    /// cref="ICareerRepository.CalculateScholarshipAsync(string)"/>  method by mocking its implementation and ensuring
    /// that the service under test correctly interacts  with the repository and returns the expected result.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task CalculateScholarshipAsync_ReturnsExpectedResult()
    {
        var name = "Computer Science";
        var expected = Result.Success();

        _careerRepositoryMock
            .Setup(repo => repo.CalculateScholarshipAsync(name))
            .ReturnsAsync(expected);

        var result = await _serviceUnderTest.CalculateScholarshipAsync(name);

        result.Should().Be(expected);
        _careerRepositoryMock.Verify(repo => repo.CalculateScholarshipAsync(name), Times.Once);
    }

    /// <summary>
    /// Tests the <see cref="ICareerRepository.SearchKeywordAsync(string)"/> method to ensure it returns the expected
    /// search results for a given keyword.
    /// </summary>
    /// <remarks>This test verifies that the <see cref="ICareerRepository.SearchKeywordAsync(string)"/> method
    /// correctly retrieves search results based on the provided keyword and that the results match the expected output.
    /// It also ensures that the repository method is called exactly once during the test.</remarks>
    /// <returns></returns>
    [Fact]
    public async Task SearchKeywordAsync_ReturnsSearchResults()
    {
        var keyword = "computing";
        var expected = new List<SearchResult>
        {   
            new SearchResult
            {
                CareerId = 1,
                CareerName = "Computer Science",
                TableName = "Career",
                Field = "Computer Science",
                ColumnName = "Name"
            }
        };


        _careerRepositoryMock
            .Setup(repo => repo.SearchKeywordAsync(keyword))
            .ReturnsAsync(expected);

        var result = await _serviceUnderTest.SearchKeywordAsync(keyword);

        result.Should().BeEquivalentTo(expected);
        _careerRepositoryMock.Verify(repo => repo.SearchKeywordAsync(keyword), Times.Once);
    }
}
