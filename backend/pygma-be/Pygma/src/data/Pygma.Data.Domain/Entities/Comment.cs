using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pygma.Data.Domain.Entities.Base;

namespace Pygma.Data.Domain.Entities
{
    public class Comment: EntityBase
    {
        [Required]
        public string VisitorName { get; set; }

        [Required]
        public string CommentText { get; set; }

        [Required]
        public int BlogPostId { get; set; }

        [ForeignKey(nameof(BlogPostId))]
        public BlogPost BlogPost { get; set; }
    }
}