namespace MediaReview.Review.Domain.Entities;

public class Tag
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public ICollection<ReviewTag> ReviewTags { get; set; } = new List<ReviewTag>();
}
