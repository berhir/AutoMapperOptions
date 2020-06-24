using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapperOptions.Module2
{
    public static class ServiceCollectionExtensions
    {
        public static void AddModule2(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<Profile1>();
                cfg.AddProfile<Profile2>();
            });

            services.AddTransient<ISomeService>(sp => new FooService(5));
            services.AddTransient<DependencyResolver>();
        }
    }
}