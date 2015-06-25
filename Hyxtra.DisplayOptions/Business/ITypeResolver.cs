using System;

namespace Hyxtra.DisplayOptions
{
    /// <summary>
    /// Interface used for implementing a type resolver.
    /// </summary>
    public interface ITypeResolver
    {
        /// <summary>
        /// Resolve the given type.
        /// </summary>
        /// <param name="fullName">The fully namespaced typename to resolve.</param>
        /// <returns>A <see cref="Type"/> if the type could be found, null otherwise.</returns>
        /// <remarks>The <paramref name="fullName"/> comparison should be case-insenstive.</remarks>
        Type ResolveType(string fullName);
    }
}
