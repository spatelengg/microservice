﻿using BuildingBlocks.CQRS;
using MediatR;
using FluentValidation;

namespace BuildingBlocks.Behaviors;

public class ValidationBehavior<TRequst, TResponse>
    (IEnumerable<IValidator<TRequst>> validators)
    : IPipelineBehavior<TRequst, TResponse>
    where TRequst : ICommand<TResponse>

{
    public async Task<TResponse> Handle(TRequst request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequst>(request);

        var validationResults 
            = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r=>r.Errors.Any())
            .SelectMany(r=>r.Errors)
            .ToList();

        if (failures.Any()) {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
