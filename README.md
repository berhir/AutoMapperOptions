# AutoMapperOptions
This repo contains a demo project to show how to configure AutoMapper using the [options pattern](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-3.1).  
What is the options pattern (from the Microsoft docs):
> The options pattern uses classes to provide strongly typed access to groups of related settings. When configuration settings are isolated by scenario into separate classes, the app adheres to two important software engineering principles:
> * The Interface Segregation Principle (ISP) or Encapsulation: Scenarios (classes) that depend on configuration settings depend only on the configuration settings that they use.
> * Separation of Concerns: Settings for different parts of the app aren't dependent or coupled to one another.

AutoMapper provides an [extension library for Microsoft.Extensions.DependencyInjection](https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection).
It uses assembly scanning to automatically register the MapperConfiguration, IMapper, profiles and implementations of some interfaces.
```cs
services.AddAutoMapper(assembly1, assembly2 /*, ...*/);
```
This extension method must be called only once in the main project and you must pass all assemblies that should be scanned.

We prefer to explicitely register all services without assembly scanning. And each library/module should register its own dependencies.
This is currently not supported by the extension library.

## Sample using the options pattern
The sample code uses custom extension methods to register and configure AutoMapper. The code can be found in the `AutoMapperOptions.Extensions.Microsoft.DependencyInjection` project.

You can call AddAutoMapper multiple times from different places/libraries and add the configuration you need:
```cs
services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<Profile1>();
    cfg.AddProfile<Profile2>();
});
```

Behind the scenes it adds the configuration to the service collection using the extension method from the `Microsoft.Extensions.Options` package:
```cs
public static IServiceCollection AddAutoMapper(this IServiceCollection services, Action<MapperConfigurationExpression> configureOptions)
{
    services.Configure(configureOptions);
    return services.AddAutoMapper();
}
```

Later on we can get the combined configration and create the `MapperConfiguration`:
```cs
var options = sp.GetRequiredService<IOptions<MapperConfigurationExpression>>();
return new MapperConfiguration(options.Value);
```

## Sample project structure
- *AutoMapperOptions:* Console app to execute the sample code
- *AutoMapperOptions.Extensions.Microsoft.DependencyInjection:* Extension methods to register and configure AutoMapper
- *AutoMapperOptions.Module1:* Sample module that registers a service and an AutoMapper profile
- *AutoMapperOptions.Module2:* Sample module that contains the code from the AutoMapper [TestApp](https://github.com/AutoMapper/AutoMapper.Extensions.Microsoft.DependencyInjection/tree/master/src/TestApp) to demonstrate the differences