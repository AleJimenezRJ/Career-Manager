using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Requests;

/// <summary>
/// Represents a request to post language data.
/// </summary>
/// <remarks>This record encapsulates the language information to be posted, as represented by the <see
/// cref="LanguageDto"/> object.</remarks>
/// <param name="Dto">The language data to be posted. This parameter must not be <see langword="null"/>.</param>
public record class PostLanguageRequest(LanguageDto Dto);