using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Mappers;

public class CareerDtoMapperTests
{
    [Fact]
    public void ToDto_Should_Map_Entity_To_AddCareerDto_Correctly()
    {
        // Arrange
        var entity = new Career(
            name: EntityName.FromDatabase("Software Engineering"),
            description: Description.FromDatabase("Engineering focused on software development."),
            semestersNumber: SemestersNumber.FromDatabase(8),
            modality: Modality.FromDatabase("Presential"),
            degreeTitle: DegreeTitle.FromDatabase("Bachelor"),
            isSteam: true
        );

        // Act
        var dto = CareerDtoMapper.ToDto(entity);

        // Assert
        dto.Name.Should().Be("Software Engineering");
        dto.Description.Should().Be("Engineering focused on software development.");
        dto.SemestersNumber.Should().Be(8);
        dto.Modality.Should().Be("Presential");
        dto.DegreeTitle.Should().Be("Bachelor");
        dto.IsSteam.Should().BeTrue();
    }

    [Fact]
    public void ToListDto_Should_Map_Entity_To_ListCareerDto_Correctly()
    {
        // Arrange
        var entity = new Career(
            careerInternalId: 101,
            name: EntityName.FromDatabase("Industrial Engineering"),
            description: Description.FromDatabase("Optimization of systems and processes."),
            semestersNumber: SemestersNumber.FromDatabase(10),
            modality: Modality.FromDatabase("Virtual"),
            degreeTitle: DegreeTitle.FromDatabase("Licentiate"),
            workInformation: new List<WorkInformation>(),
            scholarship: 2000m,
            isSteam: false
        );

        // Act
        var dto = CareerDtoMapper.ToListDto(entity);

        // Assert
        dto.CareerInternalId.Should().Be(101);
        dto.Name.Should().Be("Industrial Engineering");
        dto.Description.Should().Be("Optimization of systems and processes.");
        dto.SemestersNumber.Should().Be(10);
        dto.Modality.Should().Be("Virtual");
        dto.DegreeTitle.Should().Be("Licentiate");
        dto.Scholarship.Should().Be(2000m);
        dto.IsSteam.Should().BeFalse();
    }

    [Fact]
    public void ToEntity_Should_Return_Success_When_Dto_Is_Valid()
    {
        // Arrange
        var dto = new AddCareerDto(
            Name: "Biotechnology",
            Description: "Study of biological systems",
            SemestersNumber: 8,
            Modality: "Hybrid",
            DegreeTitle: "Bachelor",
            IsSteam: true
        );

        // Act
        var result = CareerDtoMapper.ToEntity(dto);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Name!.Name.Should().Be("Biotechnology");
        result.Value.Description!.Content.Should().Be("Study of biological systems");
        result.Value.SemestersNumber!.Number.Should().Be(8);
        result.Value.Modality!.Value.Should().Be("Hybrid");
        result.Value.DegreeTitle!.Value.Should().Be("Bachelor");
        result.Value.IsSteam.Should().BeTrue();
    }

    [Fact]
    public void ToEntity_Should_Return_Failure_When_Dto_Has_Invalid_Fields()
    {
        // Arrange: Name and Description are invalid (empty), Semesters is too low
        var dto = new AddCareerDto(
            Name: "",
            Description: "",
            SemestersNumber: 0,
            Modality: "InvalidModality",
            DegreeTitle: "",
            IsSteam: false
        );

        // Act
        var result = CareerDtoMapper.ToEntity(dto);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeNull();
    }
}
