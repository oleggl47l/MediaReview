using MediaReview.Review.Application.Review.Commands.Review;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaReview.Review.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class ReviewController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateReviewAsync(CreateReviewCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}