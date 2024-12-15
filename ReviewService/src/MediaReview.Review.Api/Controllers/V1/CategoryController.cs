using MediaReview.Review.Application.Review.Commands.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaReview.Review.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class CategoryController(IMediator mediator) : Controller
{
    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
    {
        if (command == null)
        {
            return BadRequest();
        }

        await mediator.Send(command);

        return Ok();
    }

}