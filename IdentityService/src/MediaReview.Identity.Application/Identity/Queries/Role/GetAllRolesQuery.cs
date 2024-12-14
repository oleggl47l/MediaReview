using MediatR;

namespace MediaReview.Identity.Application.Identity.Queries.Role;

public class GetAllRolesQuery : IRequest<List<Domain.Entities.Role>>;
