using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Tag;

public class DeleteTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<DeleteTagCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(request.Id);
        if (tag == null)
            throw new KeyNotFoundException($"Category with id {request.Id} not found.");

        await tagRepository.Remove(request.Id);
        return Unit.Value;
    }
}