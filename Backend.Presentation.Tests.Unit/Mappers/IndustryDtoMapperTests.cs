using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Mappers;

public class IndustryDtoMapperTests
{
    [Fact]
    public void ToDto_Should_Map_Entity_To_AddIndustryDto_Correctly()
    {
        // Arrange
        var industry = new Industry(
            Description.FromDatabase("Sector related to IT infrastructure."),
            EntityName.FromDatabase("Information Technology"),
            true
        );

        // Act
        var dto = IndustryDtoMapper.ToDto(industry);

        // Assert
        dto.InformationDescription.Should().Be("Sector related to IT infrastructure.");
        dto.Name.Should().Be("Information Technology");
        dto.CSRelated.Should().BeTrue();
    }

    [Fact]
    public void ToListDto_Should_Map_Entity_To_ListIndustryDto_Correctly()
    {
        // Arrange
        var industry = new Industry(
            Description.FromDatabase("Manufacturing and automation."),
            EntityName.FromDatabase("Manufacturing"),
            false
        )
        {
            WorkInformationInternalId = 456
        };

        // Act
        var dto = IndustryDtoMapper.ToListDto(industry);

        // Assert
        dto.WorkInformationInternalId.Should().Be(456);
        dto.InformationDescription.Should().Be("Manufacturing and automation.");
        dto.Name.Should().Be("Manufacturing");
        dto.CSRelated.Should().BeFalse();
    }

    [Fact]
    public void ToEntity_Should_Return_Success_When_Dto_Is_Valid()
    {
        // Arrange
        var dto = new AddIndustryDto(
            InformationDescription: "AI and data analysis.",
            Name: "Artificial Intelligence",
            CSRelated: true
        );

        // Act
        var result = IndustryDtoMapper.ToEntity(dto);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.InformationDescription!.Content.Should().Be("AI and data analysis.");
        result.Value.Name!.Name.Should().Be("Artificial Intelligence");
        result.Value.CSRelated.Should().BeTrue();
    }

    [Fact]
    public void ToEntity_Should_Return_Failure_When_Dto_Is_Invalid()
    {
        // Arrange: invalid name and description
        var dto = new AddIndustryDto(
            InformationDescription: "",
            Name: "",
            CSRelated: false
        );

        // Act
        var result = IndustryDtoMapper.ToEntity(dto);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
    }
}
