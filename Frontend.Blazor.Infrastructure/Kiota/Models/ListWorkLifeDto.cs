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
    public partial class ListWorkLifeDto : global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListWorkInformationDto, IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>The amountFemaleWorkers property</summary>
        public int? AmountFemaleWorkers { get; set; }
        /// <summary>The amountMaleWorkers property</summary>
        public int? AmountMaleWorkers { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListWorkLifeDto"/> and sets the default values.
        /// </summary>
        public ListWorkLifeDto() : base()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListWorkLifeDto"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static new global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListWorkLifeDto CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.Models.ListWorkLifeDto();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public override IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>(base.GetFieldDeserializers())
            {
                { "amountFemaleWorkers", n => { AmountFemaleWorkers = n.GetIntValue(); } },
                { "amountMaleWorkers", n => { AmountMaleWorkers = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public override void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            base.Serialize(writer);
            writer.WriteIntValue("amountFemaleWorkers", AmountFemaleWorkers);
            writer.WriteIntValue("amountMaleWorkers", AmountMaleWorkers);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}
#pragma warning restore CS0618
