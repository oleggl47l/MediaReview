using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Tag;

public class CreateTagCommandHandler(ITagRepository tagRepository)
    : IRequestHandler<CreateTagCommand, Unit>
{
    public async Task<Unit> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        if (await tagRepository.TagExistsByNameAsync(request.Name))
            throw new InvalidOperationException($"Tag with name '{request.Name}' already exists.");
        
        var tag = new Domain.Entities.Tag
        {
            Name = request.Name,
        };

        await tagRepository.AddAsync(tag);

        return Unit.Value;
    }
}