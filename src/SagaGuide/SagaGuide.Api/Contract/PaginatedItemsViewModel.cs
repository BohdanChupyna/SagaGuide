namespace SagaGuide.Api.Contract;

public class PaginatedItemsViewModel<TEntity> where TEntity : class
{
    public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }

    /// <summary>
    ///     Total number of matched items in the collection.
    /// </summary>
    public long Count { get; }

    /// <summary>
    ///     The items on the page.
    /// </summary>
    public IEnumerable<TEntity> Data { get; }

    /// <summary>
    ///     Zero based page index.
    /// </summary>
    public int PageIndex { get; }

    /// <summary>
    ///     Maximum number of items on a page.
    /// </summary>
    public int PageSize { get; }
}