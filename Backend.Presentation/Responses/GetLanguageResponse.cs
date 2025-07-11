using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response containing a list of languages.
/// </summary>
/// <remarks>This record is typically used to encapsulate the result of an operation that retrieves language
/// data.</remarks>
/// <param name="LanguageDtos"></param>
public record class GetLanguageResponse(List<LanguageDto> LanguageDtos);