using System.ComponentModel.DataAnnotations;
using MediaReview.Identity.Domain.Models;
using MediatR;

namespace MediaReview.Identity.Application.Identity.Queries;

public class LoginQuery : IRequest<LoginDto>
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
}