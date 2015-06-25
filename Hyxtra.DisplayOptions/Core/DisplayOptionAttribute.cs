using System;

namespace Hyxtra.DisplayOptions
{
    /// <summary>
    /// Used to control which DisplayOptions are available for a content type.
    /// </summary>
    /// <remarks>
    /// The restricted types (<see cref="Except"/>) always takes precedence.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class DisplayOptionAttribute : Attribute
    {
#pragma warning disable 1591
        public DisplayOptionAttribute()
            : this(new string[0], new string[0])
        {

        }
        public DisplayOptionAttribute(params string[] allow)
            : this(allow, new string[0])
        {

        }
        public DisplayOptionAttribute(string[] allow, string[] except)
        {
            this.Allow = allow;
            this.Except = except;
        }
#pragma warning restore 1591
        /// <summary>
        /// Gets or sets an array of allowed <see cref="EPiServer.Web.DisplayOption"/> ids.
        /// </summary>
        public string[] Allow { get; set; }
        /// <summary>
        /// Gets or sets an array of restricted <see cref="EPiServer.Web.DisplayOption"/> ids.
        /// </summary>
        public string[] Except { get; set; }
    }
}