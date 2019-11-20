using System;
using System.ComponentModel.DataAnnotations;

namespace Pygma.Data.Domain.Entities.Base
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}