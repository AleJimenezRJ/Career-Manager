// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System;
namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models
{
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    #pragma warning disable CS1591
    public partial class GetIndustryResponse : IParsable
    #pragma warning restore CS1591
    {
        /// <summary>The industries property</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public List<global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListIndustryDto>? Industries { get; set; }
#nullable restore
#else
        public List<global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListIndustryDto> Industries { get; set; }
#endif
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.GetIndustryResponse"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.GetIndustryResponse CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.GetIndustryResponse();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "industries", n => { Industries = n.GetCollectionOfObjectValues<global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListIndustryDto>(global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListIndustryDto.CreateFromDiscriminatorValue)?.AsList(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteCollectionOfObjectValues<global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListIndustryDto>("industries", Industries);
        }
    }
}
#pragma warning restore CS0618
