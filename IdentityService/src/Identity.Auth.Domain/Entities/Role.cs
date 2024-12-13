using Microsoft.AspNetCore.Identity;

namespace Identity.AuthService.Domain.Entities;

public class Role : IdentityRole
{
    public bool IsActive { get; set; }
}
