namespace Pygma.Data.Abstractions.SearchSpecifications
{
    public interface ISetCriteriaToSpecification<T>
    {
        void SetCriteria(T searchCriteria);
    }
}