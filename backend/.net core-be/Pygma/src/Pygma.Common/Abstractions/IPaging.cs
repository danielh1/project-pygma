namespace Pygma.Common.Abstractions
{
    public interface IPaging
    {
        int CurrentPage { get; set; }
        int PageSize { get; set; }
    }
}