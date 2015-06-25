using EPiServer.Data.Dynamic;
using EPiServer.Framework.Cache;
using EPiServer.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hyxtra.DisplayOptions
{    /// <summary>
    /// An implementation of <see cref="IDisplayOptionsResolver"/> using <see cref="DisplayOptionAttribute"/> to resolve display options.
    /// </summary>
    public class AttributeDisplayOptionsResolver : IDisplayOptionsResolver
    {
        const string cacheKey = "IEnumerable<DisplayOption>_";
        readonly EPiServer.Web.DisplayOptions displayOptions;
        readonly IObjectInstanceCache cache;
        /// <summary>
        /// Constructs a new instance of <see cref="AttributeDisplayOptionsResolver"/>.
        /// </summary>
        /// <param name="displayOptions">A list of available displayoptions.</param>
        /// <param name="cache">An <see cref="IObjectInstanceCache"/> object used for caching.</param>
        public AttributeDisplayOptionsResolver(EPiServer.Web.DisplayOptions displayOptions, IObjectInstanceCache cache)
        {
            this.displayOptions = displayOptions;
            this.cache = cache;
        }
        /// <summary>
        /// Get all the display options based on a type.
        /// </summary>
        /// <param name="type">The type for which DisplayOption should be resolved.</param>
        /// <returns>An <see cref="IEnumerable{DisplayOption}"/> of all the available display options.</returns>
        public IEnumerable<DisplayOption> DisplayOptionsForType(Type type)
        {
            var key = cacheKey + type.FullName;
            var cached = cache.Get(key) as IEnumerable<DisplayOption>;
            if (cached != null)
                return cached;

            // Default is to display all the display options
            IEnumerable<DisplayOption> result = displayOptions;

            var attribute = type
                .GetCustomAttributes<DisplayOptionAttribute>(true)
                .FirstOrDefault();

            // We got the attribute, so...
            if (attribute != null)
            {
                // ...remove the restricted types
                result = result.Where(option => !attribute.Except.Contains(option.Id));

                // ... and the filter on the allowed ones (if we got any)
                if (attribute.Allow.Any())
                    result = result.Where(option => attribute.Allow.Contains(option.Id));
            }

            cache.Insert(key, result, CacheEvictionPolicy.Empty);
            return result;
        }
    }
}
