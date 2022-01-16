namespace Ordering.Domain.OrderLines.Validators;

using Ordering.Dtos.OrderLine;
using FluentValidation;

public class OrderLineForUpdateDtoValidator: OrderLineForManipulationDtoValidator<OrderLineForUpdateDto>
{
    public OrderLineForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}