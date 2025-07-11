using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Mappers;

public class OpportunityDtoMapperTests
{
    [Fact]
    public void ToDto_Should_Map_Opportunity_To_AddOpportunityDto_Correctly()
    {
        // Arrange
        var opportunity = new Opportunity(
            informationDescription: Description.FromDatabase("Work abroad in research labs."),
            country: Country.FromDatabase("Germany")
        );

        // Act
        var dto = OpportunityDtoMapper.ToDto(opportunity);

        // Assert
        dto.InformationDescription.Should().Be("Work abroad in research labs.");
        dto.Country.Should().Be("Germany");
    }

    [Fact]
    public void ToListDto_Should_Map_Opportunity_To_ListOpportunityDto_Correctly()
    {
        // Arrange
        var opportunity = new Opportunity(
            informationDescription: Description.FromDatabase("Join international companies."),
            country: Country.FromDatabase("Japan")
        )
        {
            WorkInformationInternalId = 321
        };

        // Act
        var dto = OpportunityDtoMapper.ToListDto(opportunity);

        // Assert
        dto.WorkInformationInternalId.Should().Be(321);
        dto.InformationDescription.Should().Be("Join international companies.");
        dto.Country.Should().Be("Japan");
    }

    [Fact]
    public void ToEntity_Should_Return_Success_When_Dto_Is_Valid()
    {
        // Arrange
        var dto = new AddOpportunityDto(
            InformationDescription: "Explore new opportunities overseas.",
            Country: "Canada"
        );

        // Act
        var result = OpportunityDtoMapper.ToEntity(dto);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.InformationDescription!.Content.Should().Be("Explore new opportunities overseas.");
        result.Value.Country!.Value.Should().Be("Canada");
    }

    [Fact]
    public void ToEntity_Should_Return_Failure_When_Dto_Is_Invalid()
    {
        // Arrange: Empty description and country (invalid values)
        var dto = new AddOpportunityDto(
            InformationDescription: "",
            Country: ""
        );

        // Act
        var result = OpportunityDtoMapper.ToEntity(dto);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeNullOrEmpty();
    }
}
