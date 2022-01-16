namespace Ordering.Controllers.v1;

using Ordering.Domain.OrderLines.Features;
using Ordering.Dtos.OrderLine;
using Ordering.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/orderlines")]
[ApiVersion("1.0")]
public class OrderLinesController: ControllerBase
{
    private readonly IMediator _mediator;

    public OrderLinesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new OrderLine record.
    /// </summary>
    /// <response code="201">OrderLine created.</response>
    /// <response code="400">OrderLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the OrderLine.</response>
    [ProducesResponseType(typeof(OrderLineDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddOrderLine")]
    public async Task<ActionResult<OrderLineDto>> AddOrderLine([FromBody]OrderLineForCreationDto orderLineForCreation)
    {
        var command = new AddOrderLine.AddOrderLineCommand(orderLineForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetOrderLine",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single OrderLine by ID.
    /// </summary>
    /// <response code="200">OrderLine record returned successfully.</response>
    /// <response code="400">OrderLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the OrderLine.</response>
    [ProducesResponseType(typeof(OrderLineDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id}", Name = "GetOrderLine")]
    public async Task<ActionResult<OrderLineDto>> GetOrderLine(Guid id)
    {
        var query = new GetOrderLine.OrderLineQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all OrderLines.
    /// </summary>
    /// <response code="200">OrderLine list returned successfully.</response>
    /// <response code="400">OrderLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the OrderLine.</response>
    /// <remarks>
    /// Requests can be narrowed down with a variety of query string values:
    /// ## Query String Parameters
    /// - **PageNumber**: An integer value that designates the page of records that should be returned.
    /// - **PageSize**: An integer value that designates the number of records returned on the given page that you would like to return. This value is capped by the internal MaxPageSize.
    /// - **SortOrder**: A comma delimited ordered list of property names to sort by. Adding a `-` before the name switches to sorting descendingly.
    /// - **Filters**: A comma delimited list of fields to filter by formatted as `{Name}{Operator}{Value}` where
    ///     - {Name} is the name of a filterable property. You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if LikeCount or CommentCount is >10
    ///     - {Operator} is one of the Operators below
    ///     - {Value} is the value to use for filtering. You can also have multiple values (for OR logic) by using a pipe delimiter, eg.`Title@= new|hot` will return posts with titles that contain the text "new" or "hot"
    ///
    ///    | Operator | Meaning                       | Operator  | Meaning                                      |
    ///    | -------- | ----------------------------- | --------- | -------------------------------------------- |
    ///    | `==`     | Equals                        |  `!@=`    | Does not Contains                            |
    ///    | `!=`     | Not equals                    |  `!_=`    | Does not Starts with                         |
    ///    | `>`      | Greater than                  |  `@=*`    | Case-insensitive string Contains             |
    ///    | `&lt;`   | Less than                     |  `_=*`    | Case-insensitive string Starts with          |
    ///    | `>=`     | Greater than or equal to      |  `==*`    | Case-insensitive string Equals               |
    ///    | `&lt;=`  | Less than or equal to         |  `!=*`    | Case-insensitive string Not equals           |
    ///    | `@=`     | Contains                      |  `!@=*`   | Case-insensitive string does not Contains    |
    ///    | `_=`     | Starts with                   |  `!_=*`   | Case-insensitive string does not Starts with |
    /// </remarks>
    [ProducesResponseType(typeof(IEnumerable<OrderLineDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetOrderLines")]
    public async Task<IActionResult> GetOrderLines([FromQuery] OrderLineParametersDto orderLineParametersDto)
    {
        var query = new GetOrderLineList.OrderLineListQuery(orderLineParametersDto);
        var queryResponse = await _mediator.Send(query);

        var paginationMetadata = new
        {
            totalCount = queryResponse.TotalCount,
            pageSize = queryResponse.PageSize,
            currentPageSize = queryResponse.CurrentPageSize,
            currentStartIndex = queryResponse.CurrentStartIndex,
            currentEndIndex = queryResponse.CurrentEndIndex,
            pageNumber = queryResponse.PageNumber,
            totalPages = queryResponse.TotalPages,
            hasPrevious = queryResponse.HasPrevious,
            hasNext = queryResponse.HasNext
        };

        Response.Headers.Add("X-Pagination",
            JsonSerializer.Serialize(paginationMetadata));

        return Ok(queryResponse);
    }


    /// <summary>
    /// Updates an entire existing OrderLine.
    /// </summary>
    /// <response code="204">OrderLine updated.</response>
    /// <response code="400">OrderLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the OrderLine.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id}", Name = "UpdateOrderLine")]
    public async Task<IActionResult> UpdateOrderLine(Guid id, OrderLineForUpdateDto orderLine)
    {
        var command = new UpdateOrderLine.UpdateOrderLineCommand(id, orderLine);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing OrderLine record.
    /// </summary>
    /// <response code="204">OrderLine deleted.</response>
    /// <response code="400">OrderLine has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the OrderLine.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id}", Name = "DeleteOrderLine")]
    public async Task<ActionResult> DeleteOrderLine(Guid id)
    {
        var command = new DeleteOrderLine.DeleteOrderLineCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
