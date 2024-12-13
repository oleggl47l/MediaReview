using MediaReview.Identity.Application.Identity.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaReview.Identity.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result.User);
    }

}