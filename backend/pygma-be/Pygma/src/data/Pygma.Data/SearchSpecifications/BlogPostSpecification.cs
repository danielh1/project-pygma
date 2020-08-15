using Pygma.Data.Abstractions.SearchSpecifications;
using Pygma.Data.Domain.Entities;
using Pygma.Data.SearchCriteria;
using Pygma.Data.SearchSpecifications.Base;

namespace Pygma.Data.SearchSpecifications
{
    public class BlogPostSpecification : BaseSpecification<BlogPost>, ISetCriteriaToSpecification<BlogPostSc>
    {
        public void SetCriteria(BlogPostSc sc)
        {
            AddInclude(x => x.Author);

            if (!string.IsNullOrWhiteSpace(sc.Title))
            {
                Criteria.Add(x => x.Title.Contains(sc.Title));
            }

            if (!string.IsNullOrWhiteSpace(sc.AuthorLastname))
            {
                Criteria.Add(x => x.Author.LastName.Contains(sc.AuthorLastname));
            }

            #region PublishedAt

            if (sc.PublishedAtFrom.HasValue && sc.PublishedAtTo.HasValue)
            {
                Criteria.Add(x =>
                    x.PublishedAt.HasValue
                    && x.PublishedAt.Value.Date >= sc.PublishedAtFrom.Value.Date
                    && x.PublishedAt.Value.Date <= sc.PublishedAtTo.Value.Date);
            }

            if (sc.PublishedAtFrom.HasValue && !sc.PublishedAtTo.HasValue)
            {
                Criteria.Add(x => x.PublishedAt.HasValue
                                  && x.PublishedAt.Value.Date >= sc.PublishedAtFrom.Value.Date);
            }

            if (!sc.PublishedAtFrom.HasValue && sc.PublishedAtTo.HasValue)
            {
                Criteria.Add(x => x.PublishedAt.HasValue
                                  && x.PublishedAt.Value.Date <= sc.PublishedAtTo.Value.Date);
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