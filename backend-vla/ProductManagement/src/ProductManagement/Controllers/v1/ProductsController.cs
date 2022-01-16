namespace ProductManagement.Controllers.v1;

using ProductManagement.Domain.Products.Features;
using ProductManagement.Dtos.Product;
using ProductManagement.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/products")]
[ApiVersion("1.0")]
public class ProductsController: ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Product record.
    /// </summary>
    /// <response code="201">Product created.</response>
    /// <response code="400">Product has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Product.</response>
    [ProducesResponseType(typeof(ProductDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddProduct")]
    public async Task<ActionResult<ProductDto>> AddProduct([FromBody]ProductForCreationDto productForCreation)
    {
        var command = new AddProduct.AddProductCommand(productForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetProduct",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Product by ID.
    /// </summary>
    /// <response code="200">Product record returned successfully.</response>
    /// <response code="400">Product has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Product.</response>
    [ProducesResponseType(typeof(ProductDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDto>> GetProduct(Guid id)
    {
        var query = new GetProduct.ProductQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Products.
    /// </summary>
    /// <response code="200">Product list returned successfully.</response>
    /// <response code="400">Product has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Product.</response>
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
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetProducts")]
    public async Task<IActionResult> GetProducts([FromQuery] ProductParametersDto productParametersDto)
    {
        var query = new GetProductList.ProductListQuery(productParametersDto);
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
    /// Updates an entire existing Product.
    /// </summary>
    /// <response code="204">Product updated.</response>
    /// <response code="400">Product has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Product.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id}", Name = "UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(Guid id, ProductForUpdateDto product)
    {
        var command = new UpdateProduct.UpdateProductCommand(id, product);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Product record.
    /// </summary>
    /// <response code="204">Product deleted.</response>
    /// <response code="400">Product has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Product.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id}", Name = "DeleteProduct")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        var command = new DeleteProduct.DeleteProductCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
