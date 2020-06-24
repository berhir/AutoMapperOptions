using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapperOptions.Module2
{
    public static class ServiceCollectionExtensions
    {
        public static void AddModule2(this IServiceCollection services)
        {
            services.AddAutoMapper(options =>
            {
                options.AddProfile<Profile1>();
                options.AddProfile<Profile2>();
            });

            services.AddTransient<ISomeService>(sp => new FooService(5));
            services.AddTransient<DependencyResolver>();
        }
    }
}