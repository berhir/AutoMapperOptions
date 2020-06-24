using AutoMapper;

namespace AutoMapperOptions.Module1
{
    public class Model1
    {
        public string Prop1 { get; set; }

        public int Prop2 { get; set; }
    }

    internal class Model1Internal
    {
        public string Prop1 { get; set; }

        public int Prop2 { get; set; }
    }

    public class Module1Profile : Profile
    {
        public Module1Profile()
        {
            CreateMap<Model1Internal, Model1>();
        }
    }

    public class Service1
    {
        private readonly IMapper mapper;

        public Service1(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Model1 DoSomething()
        {
            var modelInternal = new Model1Internal
            {
                Prop1 = "Hello",
                Prop2 = 2,
            };

            return mapper.Map<Model1>(modelInternal);
        }
    }
}
