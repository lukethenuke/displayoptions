using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace Hyxtra.DisplayOptions
{
    /// <summary>
    /// Initialization module used for setting up dependencies.
    /// </summary>
    [InitializableModule]
    public sealed class DependencyInitializationModule : IConfigurableModule
    {
#pragma warning disable 1591
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Container.Configure(x =>
            {
                x.For<ITypeResolver>().Use<CurrentDomainTypeResolver>();
                x.For<IDisplayOptionsResolver>().Use<AttributeDisplayOptionsResolver>();
            });
        }

        #region Not used
        public void Initialize(InitializationEngine context){}
        public void Uninitialize(InitializationEngine context){}
        public void Preload(string[] parameters) { }
        #endregion
#pragma warning restore 1591
    }
}