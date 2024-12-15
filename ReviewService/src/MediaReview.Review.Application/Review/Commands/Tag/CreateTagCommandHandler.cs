using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Tag;

public class CreateTagCommandHandler(ITagRepository tagRepository)
    : IRequestHandler<CreateTagCommand, Unit>
{
    public async Task<Unit> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Domain.Entities.Tag
        {
            Name = request.Name,
        };

        await tagRepository.AddAsync(tag);

        return Unit.Value;
    }
}