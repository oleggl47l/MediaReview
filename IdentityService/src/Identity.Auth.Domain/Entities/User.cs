﻿using Microsoft.AspNetCore.Identity;

namespace Identity.AuthService.Domain.Entities;

public class User : IdentityUser
{
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpires { get; set; }
    public bool Active { get; set; } = true;
}