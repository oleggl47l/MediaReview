using MediaReview.Review.Domain.Entities;
using MediaReview.Review.Domain.Interfaces;

namespace MediaReview.Review.Infrastructure.Data.Repositories;

public class CategoryRepository(ApplicationDbContext context) : RepositoryBase<Category>(context), ICategoryRepository;