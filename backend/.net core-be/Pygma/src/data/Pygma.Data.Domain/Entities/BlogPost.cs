using System;
using System.ComponentModel.DataAnnotations;
using Pygma.Data.Domain.Entities.Base;
using Pygma.Data.Domain.Enums;

namespace Pygma.Data.Domain.Entities
{
    public class BlogPost: EntityBase
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Post { get; set; }

        public DateTime? PublishedAt { get; set; }

        [Required]
        public BlogPostStatusEnum Status { get; set; }
    }
}
