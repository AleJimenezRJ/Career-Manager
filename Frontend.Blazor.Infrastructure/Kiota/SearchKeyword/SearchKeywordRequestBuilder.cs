// <auto-generated/>
#pragma warning disable CS0618
using Microsoft.Kiota.Abstractions.Extensions;
using Microsoft.Kiota.Abstractions;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.SearchKeyword.Item;
namespace UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.SearchKeyword
{
    /// <summary>
    /// Builds and executes requests for operations under \search-keyword
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCode("Kiota", "1.0.0")]
    public partial class SearchKeywordRequestBuilder : BaseRequestBuilder
    {
        /// <summary>Gets an item from the UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.searchKeyword.item collection</summary>
        /// <param name="position">Unique identifier of the item</param>
        /// <returns>A <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.SearchKeyword.Item.WithKeywordItemRequestBuilder"/></returns>
        public global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.SearchKeyword.Item.WithKeywordItemRequestBuilder this[string position]
        {
            get
            {
                var urlTplParams = new Dictionary<string, object>(PathParameters);
                urlTplParams.Add("keyword", position);
                return new global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.SearchKeyword.Item.WithKeywordItemRequestBuilder(urlTplParams, RequestAdapter);
            }
        }
        /// <summary>
        /// Instantiates a new <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.SearchKeyword.SearchKeywordRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="pathParameters">Path parameters for the request</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SearchKeywordRequestBuilder(Dictionary<string, object> pathParameters, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/search-keyword", pathParameters)
        {
        }
        /// <summary>
        /// Instantiates a new <see cref="global::UCR.ECCI.IS.VRCampus.Frontend.Blazor.Infrastructure.Kiota.SearchKeyword.SearchKeywordRequestBuilder"/> and sets the default values.
        /// </summary>
        /// <param name="rawUrl">The raw URL to use for the request builder.</param>
        /// <param name="requestAdapter">The request adapter to use to execute the requests.</param>
        public SearchKeywordRequestBuilder(string rawUrl, IRequestAdapter requestAdapter) : base(requestAdapter, "{+baseurl}/search-keyword", rawUrl)
        {
        }
    }
}
#pragma warning restore CS0618
