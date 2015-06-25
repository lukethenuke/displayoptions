using EPiServer.Web;
using System;
using System.Collections.Generic;

namespace Hyxtra.DisplayOptions
{
    /// <summary>
    /// Interface used for implementing a display options resolver.
    /// </summary>
    public interface IDisplayOptionsResolver
    {
        /// <summary>
        /// Get all the display options based on a type.
        /// </summary>
        /// <param name="type">The type for which DisplayOption should be resolved.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="DisplayOption"/> containing all available display options for <paramref name="type"/>.</returns>
        IEnumerable<DisplayOption> DisplayOptionsForType(Type type);
    }
}
