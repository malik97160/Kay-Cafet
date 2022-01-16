namespace Ordering.Domain.Orders.Validators;

using Ordering.Dtos.Order;
using FluentValidation;

public class OrderForCreationDtoValidator: OrderForManipulationDtoValidator<OrderForCreationDto>
{
    public OrderForCreationDtoValidator()
    {
        // add fluent validation rules that should only be run on creation operations here
        //https://fluentvalidation.net/
    }
}