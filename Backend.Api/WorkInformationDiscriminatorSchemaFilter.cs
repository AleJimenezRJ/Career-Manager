using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;

namespace UCR.ECCI.IS.VRCampus.Backend.SchemaFilter;

/// <summary>
/// Schema filter for configuring OpenAPI discriminators on derived types of <see cref="ListWorkInformationDto"/>
/// and <see cref="AddWorkInformationDto"/>. This enables polymorphic serialization and deserialization in Swagger UI.
/// </summary>
public class WorkInformationDiscriminatorSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Applies the discriminator configuration to the OpenAPI schema for supported types.
    /// Specifically adds an <c>@odata.type</c> property and maps derived DTO types to their schema references.
    /// </summary>
    /// <param name="schema">The OpenAPI schema to modify.</param>
    /// <param name="context">Contextual information about the schema being generated, including the target .NET type.</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(ListWorkInformationDto))
        {
            schema.Discriminator = new OpenApiDiscriminator
            {
                PropertyName = "@odata.type",
                Mapping =
                {
                    { "#namespace.ListIndustryDto", "#/components/schemas/ListIndustryDto" },
                    { "#namespace.ListOpportunityDto", "#/components/schemas/ListOpportunityDto" },
                    { "#namespace.ListWorkLifeDto", "#/components/schemas/ListWorkLifeDto" },
                    { "#namespace.ListRecruitmentDto", "#/components/schemas/ListRecruitmentDto" },
                    { "#namespace.ListEnterpriseDto", "#/components/schemas/ListEnterpriseDto" }
                }
            };
            schema.Required.Add("@odata.type");
            schema.Properties["@odata.type"] = new OpenApiSchema { Type = "string" };
        }
        else if (context.Type == typeof(AddWorkInformationDto))
        {
            schema.Discriminator = new OpenApiDiscriminator
            {
                PropertyName = "@odata.type",
                Mapping =
                {
                    { "#namespace.AddIndustryDto", "#/components/schemas/AddIndustryDto" },
                    { "#namespace.AddOpportunityDto", "#/components/schemas/AddOpportunityDto" },
                    { "#namespace.AddWorkLifeDto", "#/components/schemas/AddWorkLifeDto" },
                    { "#namespace.AddRecruitmentDto", "#/components/schemas/AddRecruitmentDto" },
                    { "#namespace.AddEnterpriseDto", "#/components/schemas/AddEnterpriseDto" }
                }
            };
            schema.Required.Add("@odata.type");
            schema.Properties["@odata.type"] = new OpenApiSchema { Type = "string" };
        }
    }
}
