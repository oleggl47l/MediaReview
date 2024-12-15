using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Category;

public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<CreateCategoryCommand, Unit>
{
    public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Domain.Entities.Category
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow
        };

        await categoryRepository.AddAsync(category);

        return Unit.Value;
    }
}
