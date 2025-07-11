using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;

/// <summary>
/// Represents a request to create a new recruitment entry.
/// </summary>
/// <param name="Recruitment">The recruitment details to be added. This parameter must not be null.</param>
public record class PostRecruitmentRequest(AddRecruitmentDto Recruitment);