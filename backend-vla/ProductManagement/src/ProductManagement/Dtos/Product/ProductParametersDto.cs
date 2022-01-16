namespace ProductManagement.Dtos.Product;

using ProductManagement.Dtos.Shared;

public class ProductParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}