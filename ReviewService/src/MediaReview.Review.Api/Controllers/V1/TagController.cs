using MediaReview.Review.Application.Review.Commands.Tag;
using MediaReview.Review.Application.Review.Queries.Tag;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaReview.Review.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class TagController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTag(Guid id)
    {
        var tag = await mediator.Send(new GetTagByIdQuery { Id = id });
        return Ok(tag);
    }

    [HttpGet]
    public async Task<IActionResult> GetTags()
    {
        var tags = await mediator.Send(new GetAllTagsQuery());
        return Ok(tags);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateTag([FromBody] UpdateTagCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteTag([FromBody] DeleteTagCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}