namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

/// <summary>
/// Data Transfer Object (DTO) for listing careers. It includes the internal ID.
/// </summary>
/// <param name="CareerInternalId">The internal ID to the career in the system</param>
/// <param name="Name">The name given to the career</param>
/// <param name="Description">A general description for the career</param>
/// <param name="SemestersNumber">The minimum number of semester required to finish the career</param>
/// <param name="Modality">The modality of the career</param>
/// <param name="DegreeTitle">The degree title awarded after graduation</param>
public record class ListCareerDto(
    int CareerInternalId,
    string Name,
    string Description,
    int SemestersNumber,
    string Modality,
    string DegreeTitle,
    decimal Scholarship,
    bool IsSteam
);
