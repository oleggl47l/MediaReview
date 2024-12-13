﻿namespace MediaReview.Identity.Domain.Models;

public class UserDto
{
    public string Id { get; init; } 
    public string UserName { get; set; }
    public string Email { get; set; } 
    public  IEnumerable<string> Roles { get; set; }
    public bool? Active { get; set; }
}