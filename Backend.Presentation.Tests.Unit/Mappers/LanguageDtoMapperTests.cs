using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Mappers;

public class LanguageDtoMapperTests
{
    [Fact]
    public void ToDto_Should_Map_Entity_To_LanguageDto_Correctly()
    {
        // Arrange
        var entity = new Language(LanguageVO.FromDatabase("Portuguese"));

        // Act
        var dto = LanguageDtoMapper.ToDto(entity);

        // Assert
        dto.LanguageValue.Should().Be("Portuguese");
    }

    [Fact]
    public void ToEntity_Should_Return_Success_When_Dto_Is_Valid()
    {
        // Arrange
        var dto = new LanguageDto("Japanese");

        // Act
        var result = LanguageDtoMapper.ToEntity(dto);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.LanguageValue!.Value.Should().Be("Japanese");
    }

    [Fact]
    public void ToEntity_Should_Return_Failure_When_Dto_Is_Invalid()
    {
        // Arrange: empty string is invalid
        var dto = new LanguageDto("");

        // Act
        var result = LanguageDtoMapper.ToEntity(dto);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Errors.Should().NotBeEmpty();
    }
}
