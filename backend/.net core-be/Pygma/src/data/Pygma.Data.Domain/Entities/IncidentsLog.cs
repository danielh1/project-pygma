using System.ComponentModel.DataAnnotations;
using Pygma.Data.Domain.Entities.Base;
using Pygma.Data.Domain.Enums;

namespace Pygma.Data.Domain.Entities
{
    public class IncidentsLog : EntityBase
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public IncidentTypeEnum IncidentType { get; set; }
    }
}
