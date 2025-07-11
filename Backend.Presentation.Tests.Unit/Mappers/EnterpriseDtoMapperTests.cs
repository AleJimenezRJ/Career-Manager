using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Mappers;

public class EnterpriseDtoMapperTests
{
    [Fact]
    public void ToDto_Should_Map_Entity_To_AddEnterpriseDto_Correctly()
    {
        // Arrange
        var entity = new Enterprise(
            informationDescription: Description.FromDatabase("Global software consulting company."),
            name: EntityName.FromDatabase("SoftGlobal Inc."),
            country: Country.FromDatabase("Germany")
        );

        // Act
        var dto = EnterpriseDtoMapper.ToDto(entity);

        // Assert
        dto.InformationDescription.Should().Be("Global software consulting company.");
        dto.Name.Should().Be("SoftGlobal Inc.");
        dto.Country.Should().Be("Germany");
    }

    [Fact]
    public void ToListDto_Should_Map_Entity_To_ListEnterpriseDto_Correctly()
    {
        // Arrange
        var entity = new Enterprise(
            informationDescription: Description.FromDatabase("Tech R&D company."),
            name: EntityName.FromDatabase("InnoTech"),
            country: Country.FromDatabase("Japan")
        )
        {
            WorkInformationInternalId = 888
        };

        // Act
        var dto = EnterpriseDtoMapper.ToListDto(entity);

        // Assert
        dto.WorkInformationInternalId.Should().Be(888);
        dto.InformationDescription.Should().Be("Tech R&D company.");
        dto.Name.Should().Be("InnoTech");
        dto.Country.Should().Be("Japan");
    }

    [Fact]
    public void ToEntity_Should_Return_Success_When_Dto_Is_Valid()
    {
        // Arrange
        var dto = new AddEnterpriseDto(
            InformationDescription: "Biotech firm with international presence",
            Name: "BioGen",
            Country: "United States"
        );

        // Act
        var result = EnterpriseDtoMapper.ToEntity(dto);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.InformationDescription!.Content.Should().Be("Biotech firm with international presence");
        result.Value.Name!.Name.Should().Be("BioGen");
        result.Value.Country!.Value.Should().Be("United States");
    }

    [Fact]
    public void ToEntity_Should_Return_Failure_When_Dto_Is_Invalid()
    {
        // Arrange: all fields are invalid
        var dto = new AddEnterpriseDto(
            InformationDescription: "",
            Name: "",
            Country: ""
        );

        // Act
        var result = EnterpriseDtoMapper.ToEntity(dto);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
    }
}
