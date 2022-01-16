namespace ProductManagement.Dtos.Ingredient;

using ProductManagement.Dtos.Shared;

public class IngredientParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}