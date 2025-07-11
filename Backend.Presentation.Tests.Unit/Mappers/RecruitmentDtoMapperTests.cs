using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Mappers;

public class RecruitmentDtoMapperTests
{
    [Fact]
    public void ToDto_Should_Map_Entity_To_AddRecruitmentDto_Correctly()
    {
        // Arrange
        var recruitment = new Recruitment(
            informationDescription: Description.FromDatabase("Recruiting for internships."),
            steps: Description.FromDatabase("Apply -> Interview -> Decision"),
            requisites: Description.FromDatabase("Student ID, Resume"),
            languages: new List<Language>
            {
                (new Language(LanguageVO.FromDatabase("German"))),
                (new Language(LanguageVO.FromDatabase("Spanish")))
            }
        );

        // Act
        var dto = RecruitmentDtoMapper.ToDto(recruitment);

        // Assert
        dto.InformationDescription.Should().Be("Recruiting for internships.");
        dto.Steps.Should().Be("Apply -> Interview -> Decision");
        dto.Requisites.Should().Be("Student ID, Resume");
        dto.LanguageRequested.Should().HaveCount(2);
    }

    [Fact]
    public void ToListDto_Should_Map_Entity_To_ListRecruitmentDto_Correctly()
    {
        // Arrange
        var recruitment = new Recruitment(
            informationDescription: Description.FromDatabase("Recruiting researchers."),
            steps: Description.FromDatabase("Submit -> Evaluate -> Hire"),
            requisites: Description.FromDatabase("PhD, Publications"),
            languages: new List<Language>
            {
                (new Language(LanguageVO.FromDatabase("German")))
            }
        )
        {
            WorkInformationInternalId = 777
        };

        // Act
        var dto = RecruitmentDtoMapper.ToListDto(recruitment);

        // Assert
        dto.WorkInformationInternalId.Should().Be(777);
        dto.InformationDescription.Should().Be("Recruiting researchers.");
        dto.Steps.Should().Be("Submit -> Evaluate -> Hire");
        dto.Requisites.Should().Be("PhD, Publications");
        dto.LanguageRequested.Should().HaveCount(1);
    }

    [Fact]
    public void ToEntity_Should_Return_Success_When_Dto_Is_Valid()
    {
        // Arrange
        var dto = new AddRecruitmentDto(
            InformationDescription: "Join our research program",
            Steps: "Application -> Review -> Acceptance",
            Requisites: "Transcript, Recommendation Letter",
            LanguageRequested: new List<LanguageDto>
            {
                
            }
        );

        // Act
        var result = RecruitmentDtoMapper.ToEntity(dto);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.InformationDescription!.Content.Should().Be("Join our research program");
        result.Value.Steps!.Content.Should().Be("Application -> Review -> Acceptance");
        result.Value.Requisites!.Content.Should().Be("Transcript, Recommendation Letter");
    }

    [Fact]
    public void ToEntity_Should_Return_Failure_When_Dto_Is_Invalid()
    {
        // Arrange
        var dto = new AddRecruitmentDto(
            InformationDescription: "",
            Steps: "",
            Requisites: "",
            LanguageRequested: new List<LanguageDto>()
        );

        // Act
        var result = RecruitmentDtoMapper.ToEntity(dto);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
    }
}
