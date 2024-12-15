﻿using MediaReview.Review.Domain.Interfaces;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Category;

public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<UpdateCategoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            throw new KeyNotFoundException($"Category with id {request.Id} not found.");

        if (!string.IsNullOrWhiteSpace(request.Name))
            category.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Description))
            category.Description = request.Description;

        await categoryRepository.Update(category);

        return Unit.Value;
    }
}