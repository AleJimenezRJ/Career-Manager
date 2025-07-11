using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class GetKeywordHandlerTests
{
    private readonly Mock<ICareerRepository> _careerRepositoryMock;
    private readonly List<SearchResult> _searchResults;

    public GetKeywordHandlerTests()
    {
        _careerRepositoryMock = new Mock<ICareerRepository>(MockBehavior.Loose);
        _searchResults = new List<SearchResult>();
    }

    [Fact]
    public async Task HandleAsync_WhenKeywordIsValid_ShouldReturnOkWithResults()
    {
        // Arrange
        string keyword = "Technology";
        _searchResults.Add(new SearchResult
        {
            CareerId = 1,
            CareerName = "Engineering",
            TableName = "Career",
            ColumnName = "Description",
            Field = "Technology and innovation."
        });

        _careerRepositoryMock.Setup(r => r.SearchKeywordAsync(keyword))
            .ReturnsAsync(_searchResults);

        // Act
        var result = await GetKeywordHandler.HandleAsync(_careerRepositoryMock.Object, keyword);

        // Assert
        var ok = Assert.IsType<Ok<SearchKeywordResponse>>(result.Result);
        ok.Value!.Results.Should().ContainSingle(r =>
            r.CareerId == 1 &&
            r.CareerName == "Engineering" &&
            r.TableName == "Career" &&
            r.ColumnName == "Description" &&
            r.Field == "Technology and innovation."
        );

        _careerRepositoryMock.Verify(r => r.SearchKeywordAsync(keyword), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenKeywordIsNullOrEmpty_ShouldReturnBadRequest()
    {
        // Arrange
        string keyword = "";

        // Act
        var result = await GetKeywordHandler.HandleAsync(_careerRepositoryMock.Object, keyword);

        // Assert
        var badRequest = Assert.IsType<BadRequest<List<string>>>(result.Result);
        badRequest.Value.Should().ContainSingle().Which.Should().Be("The keyword must not be null or empty.");
    }

    [Fact]
    public async Task HandleAsync_WhenNoResultsFound_ShouldReturnEmptyList()
    {
        // Arrange
        string keyword = "DoesNotMatchAnything";
        _careerRepositoryMock.Setup(r => r.SearchKeywordAsync(keyword))
            .ReturnsAsync(new List<SearchResult>());

        // Act
        var result = await GetKeywordHandler.HandleAsync(_careerRepositoryMock.Object, keyword);

        // Assert
        var ok = Assert.IsType<Ok<SearchKeywordResponse>>(result.Result);
        ok.Value!.Results.Should().BeEmpty();

        _careerRepositoryMock.Verify(r => r.SearchKeywordAsync(keyword), Times.Once);
    }
}
