using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public EnBlogPostStatus Status { get; set; }
        
        [Required]
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public User Author { get; set; }

        public List<BlogPostComment> BlogPostComments { get; set; }
    }
}
