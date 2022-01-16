namespace Ordering.Domain.OrderLines.Validators;

using Ordering.Dtos.OrderLine;
using FluentValidation;

public class OrderLineForCreationDtoValidator: OrderLineForManipulationDtoValidator<OrderLineForCreationDto>
{
    public OrderLineForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}