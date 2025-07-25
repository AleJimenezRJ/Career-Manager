// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage.Item;
namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage
{
    /// <summary>
    /// Builds and executes requests for operations under \add-language
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class AddLanguageRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.addLanguage.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage.Item.WithInternalWorkInformationItemRequestBuilder"/></returns>
        public global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage.Item.WithInternalWorkInformationItemRequestBuilder this[int position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("internalWorkInformationId", position);
                return new global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage.Item.WithInternalWorkInformationItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>Gets an item from the UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.addLanguage.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage.Item.WithInternalWorkInformationItemRequestBuilder"/></returns>
        [Obsolete("This indexer is deprecated and will be removed in the next major version. Use the one with the typed parameter instead.")]
        public global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage.Item.WithInternalWorkInformationItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                if (!string.IsNullOrWhiteSpace(position)) urlTplParams.Add("internalWorkInformationId", position);
                return new global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage.Item.WithInternalWorkInformationItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage.AddLanguageRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AddLanguageRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/add-language", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.AddLanguage.AddLanguageRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public AddLanguageRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/add-language", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
