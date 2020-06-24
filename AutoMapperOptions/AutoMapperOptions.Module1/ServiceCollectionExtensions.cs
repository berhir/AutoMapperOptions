using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMapperOptions.Module1
{
    public static class ServiceCollectionExtensions
    {
        public static void AddModule1(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<Module1Profile>());

            services.AddTransient<Service1>();
        }
    }
}
