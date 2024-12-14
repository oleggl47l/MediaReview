namespace MediaReview.Identity.Api.Responses.V1;

public class UserResponse
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; } = new List<string>();
}
