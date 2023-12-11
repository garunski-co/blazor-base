using System.Collections.Generic;

namespace Spent.Commons.Dtos;

public class PagedResult<T>
{
    public IAsyncEnumerable<T>? Items { get; set; }

    public long TotalCount { get; set; }

    public PagedResult(IAsyncEnumerable<T> items, long totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public PagedResult()
    {

    }
}
