using FluentValidation.Results;

namespace DoMe.Application.Common.Exceptions;

public sealed class ValidationException : Exception
{
	public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();

	public ValidationException() : 
		base("One or more validation failures have occurred.") { }

	public ValidationException(IEnumerable<ValidationFailure> failures) : this()
	{
		Errors = failures
			.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
			.ToDictionary(eg => eg.Key, fg => fg.ToArray());
	}
}