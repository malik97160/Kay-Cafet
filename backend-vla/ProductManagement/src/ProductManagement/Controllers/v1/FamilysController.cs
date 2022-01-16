namespace ProductManagement.Controllers.v1;

using ProductManagement.Domain.Familys.Features;
using ProductManagement.Dtos.Family;
using ProductManagement.Wrappers;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

[ApiController]
[Route("api/familys")]
[ApiVersion("1.0")]
public class FamilysController: ControllerBase
{
    private readonly IMediator _mediator;

    public FamilysController(IMediator mediator)
    {
        _mediator = mediator;
    }
    

    /// <summary>
    /// Creates a new Family record.
    /// </summary>
    /// <response code="201">Family created.</response>
    /// <response code="400">Family has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Family.</response>
    [ProducesResponseType(typeof(FamilyDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost(Name = "AddFamily")]
    public async Task<ActionResult<FamilyDto>> AddFamily([FromBody]FamilyForCreationDto familyForCreation)
    {
        var command = new AddFamily.AddFamilyCommand(familyForCreation);
        var commandResponse = await _mediator.Send(command);

        return CreatedAtRoute("GetFamily",
            new { commandResponse.Id },
            commandResponse);
    }


    /// <summary>
    /// Gets a single Family by ID.
    /// </summary>
    /// <response code="200">Family record returned successfully.</response>
    /// <response code="400">Family has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Family.</response>
    [ProducesResponseType(typeof(FamilyDto), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet("{id}", Name = "GetFamily")]
    public async Task<ActionResult<FamilyDto>> GetFamily(Guid id)
    {
        var query = new GetFamily.FamilyQuery(id);
        var queryResponse = await _mediator.Send(query);

        return Ok(queryResponse);
    }


    /// <summary>
    /// Gets a list of all Familys.
    /// </summary>
    /// <response code="200">Family list returned successfully.</response>
    /// <response code="400">Family has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Family.</response>
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
    [ProducesResponseType(typeof(IEnumerable<FamilyDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpGet(Name = "GetFamilys")]
    public async Task<IActionResult> GetFamilys([FromQuery] FamilyParametersDto familyParametersDto)
    {
        var query = new GetFamilyList.FamilyListQuery(familyParametersDto);
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
    /// Updates an entire existing Family.
    /// </summary>
    /// <response code="204">Family updated.</response>
    /// <response code="400">Family has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Family.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpPut("{id}", Name = "UpdateFamily")]
    public async Task<IActionResult> UpdateFamily(Guid id, FamilyForUpdateDto family)
    {
        var command = new UpdateFamily.UpdateFamilyCommand(id, family);
        await _mediator.Send(command);

        return NoContent();
    }


    /// <summary>
    /// Deletes an existing Family record.
    /// </summary>
    /// <response code="204">Family deleted.</response>
    /// <response code="400">Family has missing/invalid values.</response>
    /// <response code="500">There was an error on the server while creating the Family.</response>
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Produces("application/json")]
    [HttpDelete("{id}", Name = "DeleteFamily")]
    public async Task<ActionResult> DeleteFamily(Guid id)
    {
        var command = new DeleteFamily.DeleteFamilyCommand(id);
        await _mediator.Send(command);

        return NoContent();
    }

    // endpoint marker - do not delete this comment
}
