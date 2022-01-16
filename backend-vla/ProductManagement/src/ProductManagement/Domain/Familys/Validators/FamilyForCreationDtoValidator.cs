namespace ProductManagement.Domain.Familys.Validators;

using ProductManagement.Dtos.Family;
using FluentValidation;

public class FamilyForCreationDtoValidator: FamilyForManipulationDtoValidator<FamilyForCreationDto>
{
    public FamilyForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}