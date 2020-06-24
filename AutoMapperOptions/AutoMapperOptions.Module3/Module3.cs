using AutoMapper;

// code from AutoMapper.Extensions.Microsoft.DependencyInjection TestApp
namespace AutoMapperOptions.Module3
{
    public class ScanSource
    {
    }

    public class ScanDest
    {
    }

    public class ScanSource2
    {
    }

    public class ScanDest2
    {
        public int ResolvedValue { get; set; }
    }

    public class ScanProfile1 : Profile
    {
        public ScanProfile1()
        {
            CreateMap<ScanSource, ScanDest>();
        }
    }

    public class ScanProfile2 : Profile
    {
        public ScanProfile2()
        {
            CreateMap<ScanSource2, ScanDest2>()
                .ForMember(d => d.ResolvedValue, opt => opt.MapFrom<ScanDependencyResolver>());
        }
    }

    public class ScanDependencyResolver : IValueResolver<object, object, int>
    {
        private readonly IScanSomeService _service;

        public ScanDependencyResolver(IScanSomeService service)
        {
            _service = service;
        }

        public int Resolve(object source, object destination, int destMember, ResolutionContext context)
        {
            return _service.Modify(destMember);
        }
    }

    public interface IScanSomeService
    {
        int Modify(int value);
    }

    public class ScanFooService : IScanSomeService
    {
        private readonly int _value;

        public ScanFooService(int value)
        {
            _value = value;
        }

        public int Modify(int value) => value + _value;
    }
}
