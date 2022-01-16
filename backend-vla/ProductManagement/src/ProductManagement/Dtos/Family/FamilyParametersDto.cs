namespace ProductManagement.Dtos.Family;

using ProductManagement.Dtos.Shared;

public class FamilyParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}