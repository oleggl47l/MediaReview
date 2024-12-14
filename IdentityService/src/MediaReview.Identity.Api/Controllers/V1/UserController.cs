﻿using MediaReview.Identity.Application.Identity.Commands.Role;
using MediaReview.Identity.Application.Identity.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace MediaReview.Identity.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class UserController (IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
    [HttpDelete("/api/v1/[controller]/[action]/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await mediator.Send(new DeleteUserCommand { UserId = id });
        if (!result)
            return NotFound(new { Message = $"User with ID {id} not found." });

        return NoContent();
    }
}