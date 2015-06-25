using EPiServer.Framework.Cache;
using System;
using System.Linq;

namespace Hyxtra.DisplayOptions
{
    /// <summary>
    /// An implementation of <see cref="ITypeResolver"/> for all the assemblies in the current domain.
    /// </summary>
    public class CurrentDomainTypeResolver : ITypeResolver
    {
        const string cacheKey = "Type_";
        readonly IObjectInstanceCache cache;
        /// <summary>
        /// Constructs a new instance of <see cref="CurrentDomainTypeResolver"/>.
        /// </summary>
        /// <param name="cache">An <see cref="IObjectInstanceCache"/> object used for caching.</param>
        public CurrentDomainTypeResolver(IObjectInstanceCache cache)
        {
            this.cache = cache;
        }
        /// <summary>
        /// Resolve the given type.
        /// </summary>
        /// <param name="fullName">The fully namespaced typename to resolve.</param>
        /// <returns>A <see cref="Type"/> if the type could be found, null otherwise.</returns>
        /// <remarks>The <paramref name="fullName"/> comparison should be case-insenstive.</remarks>
        public Type ResolveType(string fullName)
        {
            var key = cacheKey + fullName;
            var cached = cache.Get(key) as Type;
            if (cached != null)
                return cached;

            var result = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(ass => ass.GetTypes())
                .FirstOrDefault(type => type.FullName.Equals(fullName, StringComparison.OrdinalIgnoreCase));

            if (result != null)
                cache.Insert(key, result, CacheEvictionPolicy.Empty);

            return result;
        }
    }
}
