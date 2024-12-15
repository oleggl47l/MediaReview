namespace MediaReview.Review.Domain.Entities;

public class ReviewTag
{
    public Guid ReviewId { get; set; }
    public Review Review { get; set; } = null!;

    public Guid TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}