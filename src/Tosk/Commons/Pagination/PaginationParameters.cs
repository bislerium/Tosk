using System.ComponentModel.DataAnnotations;

namespace Tosk.Commons.Pagination;

public record PaginationParameters([Range(1, int.MaxValue)] int Page = default, [Range(1, int.MaxValue)] int PageSize = default)
{
    public bool IsUnpaged => Page == default && PageSize == default;
    public int Skip => (Page - 1) * PageSize;
    public int Take => PageSize;
}
