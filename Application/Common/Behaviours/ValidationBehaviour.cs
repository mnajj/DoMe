using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using ValidationException = DoMe.Application.Common.Exceptions.ValidationException;

namespace DoMe.Application.Common.Behaviours;

public sealed class ValidationBehaviour<TRequest, TResponse> :
	IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;
	
	public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
	
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		if (_validators.Any())
		{
			var validationContextContext = new ValidationContext<TRequest>(request);
			var validationResults = await Task.WhenAll(
				_validators.Select(v=> 
					v.ValidateAsync(validationContextContext, cancellationToken)));
			var validationErrors = validationResults
				.Where(r => r.Errors.Any())
				.SelectMany(e => e.Errors)
				.ToList();
			if (validationErrors.Any()) throw new ValidationException(validationErrors);
		}
		return await next();
	}
}