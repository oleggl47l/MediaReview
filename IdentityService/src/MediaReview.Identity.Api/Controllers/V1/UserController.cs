using MediaReview.Identity.Application.Identity.Commands.User;
using MediaReview.Identity.Application.Identity.Queries.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace MediaReview.Identity.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("/api/v1/[controller]/{id}")]
    public async Task<IActionResult> Get([FromRoute] string id)
    {
        var user = await mediator.Send(new GetUserQuery { UserId = id });
        return Ok(user);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpDelete("/api/v1/[controller]/[action]/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await mediator.Send(new DeleteUserCommand { UserId = id });
        return NotFound(new { Message = $"User with ID {id} not found." });
    }
}