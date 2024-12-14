﻿using MediaReview.Identity.Application.Identity.Queries;
using MediaReview.Identity.Domain.Exceptions;
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
        try
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (LoginException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (UserBlockedException ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, ex.Message);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }

}