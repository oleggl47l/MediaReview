using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Identity.Application.Identity.Commands.Role;

public class CreateRoleCommand : IRequest<Unit>
{
    [Required] public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
}