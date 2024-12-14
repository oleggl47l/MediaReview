namespace MediaReview.Identity.Domain.Models;

public class LoginModel
{
    public UserModel? User { get; set; }
    public bool Success => User != null;
}