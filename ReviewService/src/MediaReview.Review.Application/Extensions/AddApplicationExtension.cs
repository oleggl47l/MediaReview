﻿using System.Reflection;
using FluentValidation;
using MediaReview.Review.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MediaReview.Review.Application.Extensions;

public static class AddApplicationExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}