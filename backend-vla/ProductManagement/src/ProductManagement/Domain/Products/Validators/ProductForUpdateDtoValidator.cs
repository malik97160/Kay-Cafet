namespace ProductManagement.Domain.Products.Validators;

using ProductManagement.Dtos.Product;
using FluentValidation;

public class ProductForUpdateDtoValidator: ProductForManipulationDtoValidator<ProductForUpdateDto>
{
    public ProductForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}