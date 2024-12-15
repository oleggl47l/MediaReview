using MediaReview.Review.Domain.Interfaces;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Tag;

public class GetAllTagsQueryHandler(ITagRepository tagRepository)
    : IRequestHandler<GetAllTagsQuery, IEnumerable<TagModel>>
{
    public async Task<IEnumerable<TagModel>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await tagRepository.GetAllAsync();

        var tagModels = tags.Select(tag => new TagModel
        {
            Id = tag.Id,
            Name = tag.Name
        });
        
        return tagModels;
    }
}