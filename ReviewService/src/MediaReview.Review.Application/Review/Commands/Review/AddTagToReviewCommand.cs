﻿using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Review;

public class AddTagToReviewCommand : IRequest<Unit>
{
    public Guid ReviewId { get; set; }
    public Guid TagId { get; set; }
}