﻿using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MediaReview.Review.Application.Review.Commands.Category;

public class CreateCategoryCommand : IRequest<Unit>
{
    [Required] public string Name { get; set; }
    public string? Description { get; set; }
}