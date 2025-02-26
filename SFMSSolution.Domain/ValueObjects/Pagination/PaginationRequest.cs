using SFMSSolution.Domain.ValueObjects.Common;

namespace SFMSSolution.Domain.ValueObjects.Pagination;

public class PaginationRequest
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}