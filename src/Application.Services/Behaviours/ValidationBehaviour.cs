namespace Application.Services.Behaviours
{
    using FluentValidation;
    using MediatR;

    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Perform validation using injected validators
            var context = new ValidationContext<TRequest>(request);
            var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            // If validation fails, return validation errors
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            // Proceed to the next behavior or handler
            return await next();
        }
    }
}