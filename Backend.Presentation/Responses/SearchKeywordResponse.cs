using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response for a keyword search operation.
/// </summary>
/// <param name="Results">A list of results found from the search operation.</param>
public record class SearchKeywordResponse(List<SearchResultDto> Results);
