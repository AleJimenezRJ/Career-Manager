using FluentAssertions;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ValueObjects;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Mappers;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Mappers;

/// <summary>
/// Unit tests for <see cref="WorkLifeDtoMapper"/> to verify correct mapping between DTOs and entities.
/// </summary>
public class WorkLifeDtoMapperTests
{
    [Fact]
    public void ToDto_ShouldMapWorkLifeToAddWorkLifeDto()
    {
        var workLife = CreateValidWorkLife();

        var dto = WorkLifeDtoMapper.ToDto(workLife);

        dto.InformationDescription.Should().Be("Teamwork and time management");
        dto.AmountFemaleWorkers.Should().Be(20);
        dto.AmountMaleWorkers.Should().Be(25);
    }

    [Fact]
    public void ToListDto_ShouldMapWorkLifeToListWorkLifeDto()
    {
        var workLife = CreateValidWorkLife();
        workLife.WorkInformationInternalId = 123;

        var dto = WorkLifeDtoMapper.ToListDto(workLife);

        dto.WorkInformationInternalId.Should().Be(123);
        dto.InformationDescription.Should().Be("Teamwork and time management");
        dto.AmountFemaleWorkers.Should().Be(20);
        dto.AmountMaleWorkers.Should().Be(25);
    }

    [Fact]
    public void ToEntity_WithValidDto_ShouldReturnSuccessResult()
    {
        var dto = new AddWorkLifeDto("Teamwork and time management", 20, 25);

        var result = WorkLifeDtoMapper.ToEntity(dto);

        result.IsSuccess.Should().BeTrue();
        result.Value!.InformationDescription!.Content.Should().Be("Teamwork and time management");
        result.Value.AmountFemaleWorkers!.Number.Should().Be(20);
        result.Value.AmountMaleWorkers!.Number.Should().Be(25);
    }

    [Fact]
    public void ToEntity_WithInvalidDto_ShouldReturnFailureResult()
    {
        var dto = new AddWorkLifeDto("", -1, -2); // invalid fields

        var result = WorkLifeDtoMapper.ToEntity(dto);

        result.IsFailure.Should().BeTrue();
    }

    private static WorkLife CreateValidWorkLife()
    {
        return new WorkLife(
            Description.FromDatabase("Teamwork and time management"),
            Workers.FromDatabase(20),
            Workers.FromDatabase(25)
        )
        {
            WorkInformationInternalId = 0
        };
    }
}
