namespace Pygma.Data.Abstractions.SearchSpecifications
{
    public interface ISearchableSpecification<TCriteria>
    {
        void ApplyPagingAndOrder(TCriteria criteria);

    }
}