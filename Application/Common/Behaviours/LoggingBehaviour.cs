using DoMe.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace DoMe.Application.Common.Behaviours;

public sealed class LoggingBehaviour<TRequest> : 
	IRequestPreProcessor<TRequest> where TRequest : notnull
{
	private readonly ILogger _logger;
	private readonly ICurrentUserService _currentUserService;
	private readonly IIdentityService _identityService;

	public LoggingBehaviour(
		ILogger logger,
		ICurrentUserService currentUserService,
		IIdentityService identityService)
		=> (_logger, _currentUserService, _identityService) =
			(logger, currentUserService, identityService);

	public async Task Process(TRequest request, CancellationToken cancellationToken)
	{
		var requestName = typeof(TRequest).Name;
		var userId = _currentUserService.UserId ?? string.Empty;
		string? userName = string.Empty;
		if (!string.IsNullOrEmpty(userId))
		{
			userName = await _identityService.GetUserNameAsync(userId);
		}
		_logger.LogInformation(
			"Application Layer Request: {Name} {@UserId} {@UserName} {@Request}",
			requestName, userId, userName, request);
	}
}