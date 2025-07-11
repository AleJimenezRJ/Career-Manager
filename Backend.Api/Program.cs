using UCR.ECCI.IS.VRCampus.Backend.DependencyInjection;
using UCR.ECCI.IS.VRCampus.Backend.Presentation.Dtos;
using UCR.ECCI.IS.VRCampus.Backend.SchemaFilter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.SchemaFilter<WorkInformationDiscriminatorSchemaFilter>();
    x.UseAllOfForInheritance();
    x.SelectSubTypesUsing(baseType => {
        if (baseType == typeof(ListWorkInformationDto))
        {
            return new[]
            {
                typeof(ListIndustryDto),
                typeof(ListOpportunityDto),
                typeof(ListWorkLifeDto),
                typeof(ListRecruitmentDto),
                typeof(ListEnterpriseDto)
            };
        }

        if (baseType == typeof(AddWorkInformationDto))
        {
            return new[]
            {
                typeof(AddIndustryDto),
                typeof(AddOpportunityDto),
                typeof(AddWorkLifeDto),
                typeof(AddRecruitmentDto),
                typeof(AddEnterpriseDto)
            };
        }

        return Enumerable.Empty<Type>();
    });
});


builder.Services.AddCleanArchitectureServices(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(x =>
    {
        x.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.MapEndpoints();

await app.RunAsync();
