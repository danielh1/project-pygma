using TrFoil.Backbone.Common.Abstractions;

namespace TrFoil.Backbone.Common.Models.Base
{
    public class BaseVm : IBaseVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
