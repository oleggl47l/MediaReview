using MediaReview.Review.Domain.Interfaces;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Category;

public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
    : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryModel>>
{
    public async Task<IEnumerable<CategoryModel>> Handle(GetAllCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync();

        var categoryModels = categories.Select(category => new CategoryModel
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description
        });

        return categoryModels;
    }
}