using Microsoft.AspNetCore.Identity;

namespace MediaReview.Identity.Domain.Entities;

public class Role : IdentityRole
{
    public bool IsActive { get; set; }
}
