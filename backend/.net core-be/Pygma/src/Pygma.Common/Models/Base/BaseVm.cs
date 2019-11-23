using Pygma.Common.Abstractions;

namespace Pygma.Common.Models.Base
{
    public class BaseVm : IBaseVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
