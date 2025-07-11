using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class GetEnterpriseHandlerTests
{
    private readonly Mock<IEnterpriseServices> _enterpriseServiceMock;
    private readonly List<Enterprise> _enterpriseData;

    public GetEnterpriseHandlerTests()
    {
        _enterpriseServiceMock = new Mock<IEnterpriseServices>(MockBehavior.Loose);
        _enterpriseData = new List<Enterprise>();
    }

    [Fact]
    public async Task HandleAsync_WhenEnterprisesExist_ShouldReturnOkWithList()
    {
        // Arrange
        var testEnterprise = TestEnterprise("Intel");
        _enterpriseData.Add(testEnterprise);

        _enterpriseServiceMock.Setup(s => s.ListEnterpriseAsync())
            .ReturnsAsync(_enterpriseData);

        // Act
        var result = await GetEnterpriseHandler.HandleAsync(_enterpriseServiceMock.Object);

        // Assert
        var ok = Assert.IsType<Ok<GetEnterpriseListResponse>>(result.Result);
        ok.Value!.Enterprise.Should().ContainSingle(e => e.Name == "Intel");

        _enterpriseServiceMock.Verify(s => s.ListEnterpriseAsync(), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_WhenNoEnterprisesExist_ShouldReturnNotFound()
    {
        // Arrange
        _enterpriseServiceMock.Setup(s => s.ListEnterpriseAsync())
            .ReturnsAsync(_enterpriseData); // Empty

        // Act
        var result = await GetEnterpriseHandler.HandleAsync(_enterpriseServiceMock.Object);

        // Assert
        var notFound = Assert.IsType<NotFound<Error>>(result.Result);
        notFound.Value.Should().NotBeNull();
        notFound.Value!.Code.Should().Be("Data.NotFound");

        _enterpriseServiceMock.Verify(s => s.ListEnterpriseAsync(), Times.Once);
    }

    private static Enterprise TestEnterprise(string name)
    {
        return new Enterprise(
            1,
            Description.FromDatabase("Tech company"),
            EntityName.FromDatabase(name),
            Country.FromDatabase("Costa Rica")
        );
    }
}
