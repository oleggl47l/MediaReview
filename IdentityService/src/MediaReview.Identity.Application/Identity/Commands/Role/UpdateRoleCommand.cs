using System.ComponentModel.DataAnnotations;
using MediaReview.Identity.Domain.Models;
using MediatR;

namespace MediaReview.Identity.Application.Identity.Commands.Role;

public class UpdateRoleCommand : IRequest<RoleModel>
{
    [Required] public string RoleId { get; set; } = null!;

    public string? Name { get; set; }

    public bool? IsActive { get; set; }
}