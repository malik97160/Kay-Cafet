namespace Ordering.Dtos.Order;

using Ordering.Dtos.Shared;

public class OrderParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}