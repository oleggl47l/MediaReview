namespace MediaReview.Identity.Domain.Models;

public class LoginDto
{
    public UserDto? User { get; set; }
    public bool Success => User != null;
}