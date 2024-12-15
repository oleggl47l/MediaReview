using System.ComponentModel.DataAnnotations;
using MediaReview.Review.Domain.Models;
using MediatR;

namespace MediaReview.Review.Application.Review.Queries.Tag;

public class GetTagByIdQuery : IRequest<TagModel>
{
    [Required] public Guid Id { get; set; }
}