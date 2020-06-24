using AutoMapper;
using AutoMapperOptions.Module1;
using AutoMapperOptions.Module2;
using AutoMapperOptions.Module3;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AutoMapperOptions
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddModule1();
            services.AddModule2();
            services.AddAutoMapper(typeof(ScanSource));
            services.AddTransient<IScanSomeService>(sp => new ScanFooService(6));

            var provider = services.BuildServiceProvider();

            // resolve and use service from Module1
            var svc1 = provider.GetService<Service1>();
            var model1 = svc1.DoSomething();
            Console.WriteLine($"Service1 result: {model1.Prop1}, {model1.Prop2}");

            // code from AutoMapper.Extensions.Microsoft.DependencyInjection TestApp
            // services are defined in Module2 and Module3
            // servcies of Module2 are registered manually and configured via options; services of Module3 via assembly scanning
            using (var scope = provider.CreateScope())
            {
                var mapper = scope.ServiceProvider.GetService<IMapper>();

                foreach (var typeMap in mapper.ConfigurationProvider.GetAllTypeMaps())
                {
                    Console.WriteLine($"{typeMap.SourceType.Name} -> {typeMap.DestinationType.Name}");
                }

                foreach (var service in services)
                {
                    Console.WriteLine(service.ServiceType + " - " + service.ImplementationType);
                }

                var dest = mapper.Map<Dest2>(new Source2());
                Console.WriteLine(dest.ResolvedValue);

                var scanDest = mapper.Map<ScanDest2>(new ScanSource2());
                Console.WriteLine(scanDest.ResolvedValue);
            }

            Console.ReadKey(true);
        }
    }
}
