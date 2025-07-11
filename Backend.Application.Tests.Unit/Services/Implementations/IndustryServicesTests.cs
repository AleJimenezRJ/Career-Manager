using FluentAssertions;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services.Implementations;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Repositories;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;

namespace UCR.ECCI.IS.VRCampus.Backend.Application.Tests.Unit.Services.Implementations;

public class IndustryServicesTests
{
    private readonly Mock<IIndustryRepository> _industryRepositoryMock;
    private readonly IndustryServices _serviceUnderTest;
    private readonly Industry _industry;

    public IndustryServicesTests()
    {
        _industryRepositoryMock = new Mock<IIndustryRepository>(MockBehavior.Strict);
        _serviceUnderTest = new IndustryServices(_industryRepositoryMock.Object);

        _industry = new Industry
        (
            Description.FromDatabase("Software development and tech industry."),
            EntityName.FromDatabase("Tech Industry"),
            true
        );
    }

    [Fact]
    public async Task ListIndustriesAsync_ReturnsIndustryList()
    {
        var industries = new List<Industry> { _industry };

        _industryRepositoryMock
            .Setup(repo => repo.ListAllAsync())
            .ReturnsAsync(industries);

        var result = await _serviceUnderTest.ListIndustriesAsync();

        result.Should().ContainSingle().Which.Should().Be(_industry);
        _industryRepositoryMock.Verify(repo => repo.ListAllAsync(), Times.Once);
    }

    [Fact]
    public async Task AddIndustriesAsync_ReturnsSuccess_WhenRepositoryReturnsSuccess()
    {
        int careerId = 1;
        var expected = Result.Success();

        _industryRepositoryMock
            .Setup(repo => repo.AddInformationAsync(careerId, _industry))
            .ReturnsAsync(expected);

        var result = await _serviceUnderTest.AddIndustriesAsync(careerId, _industry);

        result.Should().Be(expected);
        _industryRepositoryMock.Verify(repo => repo.AddInformationAsync(careerId, _industry), Times.Once);
    }
}
