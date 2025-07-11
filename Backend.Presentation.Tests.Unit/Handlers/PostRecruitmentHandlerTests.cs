using FluentAssertions;
using Moq;
using Microsoft.AspNetCore.Http.HttpResults;
using UCR.ECCI.IS.VRCampus.Backend.Application.Services;
using UCR.ECCI.IS.VRCampus.Backend.Domain.Entities;
using UCR.ECCI.IS.VRCampus.Backend.Domain.ResultPattern;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Handlers;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Tests.Unit.Handlers;

public class PostRecruitmentHandlerTests
{
    private readonly Mock<IRecruitmentServices> _mockService;

    public PostRecruitmentHandlerTests()
    {
        _mockService = new Mock<IRecruitmentServices>(MockBehavior.Strict);
    }

    [Fact]
    public async Task HandleAsync_ReturnsOk_WhenRecruitmentCreatedSuccessfully()
    {
        // Arrange
        var recruitmentDto = new AddRecruitmentDto(
            "Descripción general",
            "Pasos a seguir",
            "Requisitos necesarios",
            new List<LanguageDto> { new("Spanish"), new("English") }
        );
        var request = new PostRecruitmentRequest(recruitmentDto);

        _mockService
            .Setup(s => s.AddRecruitmentAsync(It.IsAny<int>(), It.IsAny<Recruitment>()))
            .ReturnsAsync(Result.Success());

        // Act
        var result = await PostRecruitmentHandler.HandleAsync(_mockService.Object, request, 123);

        // Assert
        var ok = result.Result as Ok<PostResponse>;
        ok.Should().NotBeNull();
        ok!.Value!.Response.Should().Be("Recruitment successfully created.");
    }

    [Fact]
    public async Task HandleAsync_ReturnsConflict_WhenServiceFails()
    {
        // Arrange
        var recruitmentDto = new AddRecruitmentDto(
            "Descripción general",
            "Pasos a seguir",
            "Requisitos necesarios",
            new List<LanguageDto> { new("Spanish") }
        );
        var request = new PostRecruitmentRequest(recruitmentDto);

        var error = Error.DuplicatedConflict("Recruitment.Duplicate", "Already exists");

        _mockService
            .Setup(s => s.AddRecruitmentAsync(It.IsAny<int>(), It.IsAny<Recruitment>()))
            .ReturnsAsync(Result.Failure(error));

        // Act
        var result = await PostRecruitmentHandler.HandleAsync(_mockService.Object, request, 123);

        // Assert
        var conflict = result.Result as Conflict<Error>;
        conflict.Should().NotBeNull();
        conflict!.Value!.Code.Should().Be("Recruitment.Duplicate");
    }
}
