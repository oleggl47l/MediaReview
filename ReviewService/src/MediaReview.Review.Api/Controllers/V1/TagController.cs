using MediaReview.Review.Application.Review.Commands.Tag;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaReview.Review.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class TagController(IMediator mediator) : ControllerBase
{
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
}