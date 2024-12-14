using MediaReview.Identity.Application.Identity.Commands.Role;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace MediaReview.Identity.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class RoleController (IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await mediator.Send(new DeleteRoleCommand() { RoleId = id });
        return Ok();
    }
}