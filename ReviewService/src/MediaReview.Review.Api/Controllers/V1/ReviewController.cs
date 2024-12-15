using MediaReview.Review.Application.Review.Commands.Review;
using MediaReview.Review.Application.Review.Queries.Review;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaReview.Review.Api.Controllers.V1;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class ReviewController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewByIdAsync(Guid id)
    {
        var review = await mediator.Send(new GetReviewByIdQuery { Id = id });
        return Ok(review);
    }

    [HttpGet]
    public async Task<IActionResult> GetReviewsAsync()
    {
        var reviews = await mediator.Send(new GetAllReviewsQuery());
        return Ok(reviews);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateReviewAsync(CreateReviewCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteReviewAsync(DeleteReviewCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}