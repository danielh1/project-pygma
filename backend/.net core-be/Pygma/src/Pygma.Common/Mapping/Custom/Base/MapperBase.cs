using AutoMapper;

namespace Pygma.Common.Mapping.Custom.Base
{
    public abstract class MapperBase
    {
        protected MapperBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper { get; }
    }
}
