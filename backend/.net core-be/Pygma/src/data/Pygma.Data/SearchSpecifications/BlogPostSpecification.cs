using Pygma.Data.Abstractions.SearchSpecifications;
using Pygma.Data.Domain.Entities;
using Pygma.Data.SearchCriteria;
using Pygma.Data.SearchSpecifications.Base;

namespace Pygma.Data.SearchSpecifications
{
    public class BlogPostSpecification: BaseSpecification<BlogPost>, ISetCriteriaToSpecification<BlogPostSc>
    {
        public void SetCriteria(BlogPostSc sc)
        {
            
            if (!string.IsNullOrWhiteSpace(sc.Title))
            {
                Criteria.Add(x => x.Title.Contains(sc.Title));
            }
            
            if (!string.IsNullOrWhiteSpace(sc.AuthorLastname))
            {
                AddInclude(x => x.Author);
                Criteria.Add(x => x.Author.LastName.Contains(sc.AuthorLastname));
            }
        
            #region PublishedAt
            if (sc.PublishedAtFrom.HasValue && sc.PublishedAtTo.HasValue)
            {
                Criteria.Add(x => x.PublishedAt > sc.PublishedAtFrom && x.PublishedAt < sc.PublishedAtTo);
            }

            if (sc.PublishedAtFrom.HasValue && !sc.PublishedAtTo.HasValue)
            {
                Criteria.Add(x => x.PublishedAt >= sc.PublishedAtFrom);
            }

            if (!sc.PublishedAtFrom.HasValue && sc.PublishedAtTo.HasValue)
            {
                Criteria.Add(x => x.PublishedAt <= sc.PublishedAtTo);
            }
            #endregion
            
            if (sc.Status.HasValue)
            {
                Criteria.Add(x => x.Status == sc.Status);
            }
            
            ApplyPagingAndOrder(sc);
        }
    }
}