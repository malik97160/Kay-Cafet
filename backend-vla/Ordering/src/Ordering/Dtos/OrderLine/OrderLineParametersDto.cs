namespace Ordering.Dtos.OrderLine;

using Ordering.Dtos.Shared;

public class OrderLineParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}