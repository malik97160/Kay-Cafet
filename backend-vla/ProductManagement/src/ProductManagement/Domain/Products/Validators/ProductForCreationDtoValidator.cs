namespace ProductManagement.Domain.Products.Validators;

using ProductManagement.Dtos.Product;
using FluentValidation;

public class ProductForCreationDtoValidator: ProductForManipulationDtoValidator<ProductForCreationDto>
{
    public ProductForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}