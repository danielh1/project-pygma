using AutoMapper;

namespace TrFoil.Backbone.Common.Mapping.Custom
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
