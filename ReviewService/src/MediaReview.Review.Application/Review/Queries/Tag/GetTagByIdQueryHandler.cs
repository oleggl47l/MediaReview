﻿using MediaReview.Review.Domain.Interfaces;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Tag;

public class GetTagByIdQueryHandler(ITagRepository tagRepository) : IRequestHandler<GetTagByIdQuery, TagModel>
{
    public async Task<TagModel> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(request.Id);
        if (tag == null)
            throw new KeyNotFoundException($"Tag with id {request.Id} not found");

        return new TagModel
        {
            Id = tag.Id,
            Name = tag.Name,
        };
    }
}