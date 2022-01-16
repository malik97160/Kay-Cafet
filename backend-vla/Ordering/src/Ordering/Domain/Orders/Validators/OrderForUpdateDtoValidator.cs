namespace Ordering.Domain.Orders.Validators;

using Ordering.Dtos.Order;
using FluentValidation;

public class OrderForUpdateDtoValidator: OrderForManipulationDtoValidator<OrderForUpdateDto>
{
    public OrderForUpdateDtoValidator()
    {
        // add fluent validation rules that should only be run on update operations here
        //https://fluentvalidation.net/
    }
}