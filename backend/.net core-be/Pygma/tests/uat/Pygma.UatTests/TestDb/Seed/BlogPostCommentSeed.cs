using System;
using System.Collections.Generic;
using Pygma.Data;
using Pygma.Data.Domain.Entities;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.TestDb.Seed
{
    public class BlogPostCommentSeed : ISeeder
    {
        public void Seed(PygmaDbContext dbContext)
        {
            dbContext.SetInsertIdentity("BlogPostComments", true);
            
            CreateBlogPostComment(dbContext, SeedConstants.PublishedBlogPostComment1, "testVisitor_1", 
                "My Test Comment 1", SeedConstants.PublishedBlogPost);
            CreateBlogPostComment(dbContext, SeedConstants.PublishedBlogPostComment2, "testVisitor_2", 
                "My Test Comment 2", SeedConstants.PublishedBlogPost);
            CreateBlogPostComment(dbContext, SeedConstants.InEditBlogPostComment, "testVisitor_3", 
                "My Test Comment 3", SeedConstants.InEditBlogPost);
            
            dbContext.SaveChanges();
            dbContext.SetInsertIdentity("BlogPostComments", false);
            dbContext.SaveChanges();
        }
        
        private static void CreateBlogPostComment(PygmaDbContext dbContext,
            int id,
            string visitorName,
            string comment,
            int blogPostId)
        {
            dbContext.BlogPostComments.Add(new BlogPostComment()
            {
                Id = id,
                VisitorName = visitorName,
                Comment = comment,
                BlogPostId = blogPostId,
                CreatedAt = DateTime.Now
            });
        }
    }
}