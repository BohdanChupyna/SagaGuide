using MediatR;
using SagaGuide.Api.Contract;

namespace SagaGuide.Api.Queries;

public class GetPaginatedBaseQuery<TEntity> : IRequest<PaginatedItemsViewModel<TEntity>> where TEntity : class
{
    /// <summary>
    ///     Zero based page index
    /// </summary>
    public int PageIndex { get; set; } = 0;

    public int PageSize { get; set; } = 0;

    //public long Count { get; set; } = 0;
}