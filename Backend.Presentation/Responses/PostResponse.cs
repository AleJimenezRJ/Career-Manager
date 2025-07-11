namespace UCR.ECCI.IS.VRCampus.Backend.Presentation.Responses;

/// <summary>
/// Represents the response returned after a post operation, such as creating a new entity or resource.
/// </summary>
/// <param name="Response">The response message indicating the result of the operation.</param>
public record class PostResponse(string Response);