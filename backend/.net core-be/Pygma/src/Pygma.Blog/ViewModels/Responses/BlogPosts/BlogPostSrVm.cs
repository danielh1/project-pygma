using System;
using System.Text.Json.Serialization;

namespace Pygma.Blog.ViewModels.Responses.BlogPosts
{
    public class BlogPostSrVm
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        
        [JsonPropertyName("title")]
        public string Title { get; set; }
        
        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; }
        
        [JsonPropertyName("publishedAt")]
        public DateTime? PublishedAt { get; set; }
    }
}