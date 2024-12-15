namespace MediaReview.Review.Domain.Entities;

public class Review
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid AuthorId { get; private set; }
    public Guid CategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsPublished { get; private set; } = false;
    
    public Category? Category { get; set; }
    public ICollection<ReviewTag> ReviewTags { get; set; } = new List<ReviewTag>();

}