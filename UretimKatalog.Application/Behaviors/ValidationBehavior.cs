using FluentValidation;
using MediatR;

namespace UretimKatalog.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
            : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
            => _validators = validators;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken ct)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var failures = (await Task.WhenAll(
                                    _validators.Select(v => v.ValidateAsync(context, ct))))
                                .SelectMany(r => r.Errors)
                                .Where(f => f != null)
                                .ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }
            return await next();
        }
    }
}