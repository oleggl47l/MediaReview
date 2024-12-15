using MediaReview.Review.Domain.Entities;
using MediaReview.Review.Domain.Interfaces;

namespace MediaReview.Review.Infrastructure.Data.Repositories;

public class TagRepository(ApplicationDbContext context) : RepositoryBase<Tag>(context), ITagRepository;