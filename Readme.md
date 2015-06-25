# hyxtra.displayoptions

Display options in EPiServer allows the editor (human one, at the keyboard) to easily change how a piece of content is rendered. The downside is that they are applied globally, which is problematic because:
The editor does not know what display options works with which content, resulting in a lot of choosable display options (and a lot of "Missing renderer" or same-looking fallback renderers), causing frustration and losing readability.
This may be solved by only using a small set of display options which can be used for all types (eg. "small", "medium", "large"), but doing so sacrifices flexibility.

This library tries to address these shortcomings by allowing the developer to specify what display options is available for which content.

## Installation

[Simply download it through nuget...](https://www.nuget.org/packages/Hyxtra.DisplayOptions/)

...or do it the oldschool way!

1. Pull
2. Build
3. Copy *Hyxtra.DisplayOptions.dll* to your project's *bin* directory
4. Copy *modules* to your project's *modules* directory

## Getting started

Setup your display options as normal ([EPiServer World](http://world.episerver.com/documentation/Items/Developers-Guide/EPiServer-CMS/8/Rendering/Display-options/Display-options/)).
Apply ``Hyxtra.DisplayOptions.DisplayOptionAttribute`` to your models.
```c#
[DisplayOption("promo")]
public class DefaultBlock : BlockData { }
```
In case the attribute is missing from a model, the library will fallback to all display options (default EPiServer behaviour).

## Configuration

The DisplayOptionAttribute can allow or deny a display option of being applied. Denying always takes precedence. The following examples are all the same (given there are three displayoptions named 'do1', 'do2', 'do3'). 
```c#
[DisplayOption("do1", "do2")]
public class DefaultBlock : BlockData { }

[DisplayOption(Allow = new[] {"do1", "do2"})]
public class DefaultBlock : BlockData { }

[DisplayOption(Except = new[] { "do3" })]
public class DefaultBlock : BlockData { }

[DisplayOption("do1", "do2", Except = new[] { "do3" })]
public class DefaultBlock : BlockData { }
```

## Extensibility

By default this library will look for types in all assemblies in the current domain and manage display options based on attributes.
If you want more control over how the display options or types are resolved consider implementing ``ITypeResolver`` and/or ``IDisplayOptionsResolver``. Then in your initialization module take an module dependency on ``Hyxtra.DisplayOptions.DependencyInitializationModule`` and configure your dependencies.

```c#
    [InitializableModule]
    [ModuleDependency(typeof(Hyxtra.DisplayOptions.DependencyInitializationModule))]
    public sealed class InitializationModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Container.Configure(x =>
            {
                x.For<ITypeResolver>().Use<MyTypeResolver>();
                x.For<IDisplayOptionsResolver>().Use<MyDisplayOptionsResolver>();
            });
        }
    }
```

## Performance

The default implementations does not perform much work, have basic caching enabled, and should not impact performance significantly.
Consider implementing the interfaces under [Extensibility](#Extensibility)  with more performant or domain specific ones if desired. 

## Security

This library only hides the display options visually on the client-side, no runtime checks or policy is applied server-side based on the allowed or disallowed display options.

## Compatibility

I honestly have no fucking clue. Episerver add-on development is a very dark and undocumented art. I tried 7.6.3 and 8.6.0 so assuming everything in-between. It shouldn't be _that_ hard to modify it incase of breakage.   

## Todo / Ideas

Admin mode control - Admi Gui plugin/edit content type & DynamicDataStoreDisplayOptionsResolver?