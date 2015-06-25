using EPiServer.Cms.Shell.UI.Rest.Models;
using EPiServer.Data.Dynamic;
using EPiServer.Framework.Cache;
using EPiServer.Framework.Localization;
using EPiServer.Shell.Services.Rest;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;

namespace Hyxtra.DisplayOptions
{
    /// <summary>
    /// A store for fetching display options
    /// </summary>
    [RestStore("displayoptions")]
    public class DisplayOptionsStore : RestControllerBase
    {
        readonly IDisplayOptionsResolver displayOptionsResolver;
        readonly LocalizationService localizationService;
        readonly ITypeResolver typeResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayOptionsStore"/> class.
        /// </summary>
        public DisplayOptionsStore(
            IDisplayOptionsResolver displayOptionsResolver,
            LocalizationService localizationService,
            ITypeResolver typeResolver)
        {
            this.displayOptionsResolver = displayOptionsResolver;
            this.localizationService = localizationService;
            this.typeResolver = typeResolver;
        }

        /// <summary>
        /// Get the available display options for a type.
        /// </summary>
        /// <param name="id">The name of the type.</param>
        public ActionResult Get(string id)
        {
            var type = typeResolver.ResolveType(id);
            if (type == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var result = displayOptionsResolver
                .DisplayOptionsForType(type)
                .Select(option => new DisplayOptionModel(option, localizationService));

            return Rest(result);
        }
    }
}