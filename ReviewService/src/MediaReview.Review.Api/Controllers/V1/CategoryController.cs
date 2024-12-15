﻿using MediaReview.Review.Application.Review.Commands.Category;
using MediaReview.Review.Application.Review.Queries.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaReview.Review.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class CategoryController(IMediator mediator) : Controller
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryAsync(Guid id)
    {
        var category = await mediator.Send(new GetCategoryByIdQuery { Id = id });
        return Ok(category);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
}