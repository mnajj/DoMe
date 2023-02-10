using System.Diagnostics;
using DoMe.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DoMe.Application.Common.Behaviours;

public sealed class PerformanceBehaviour<TRequest, TResponse> :
	IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly Stopwatch _timer;
	private readonly ILogger<TRequest> _logger;
	private readonly ICurrentUserService _currentUserService;
	private readonly IIdentityService _identityService;

	public PerformanceBehaviour(
		ILogger<TRequest> logger,
		ICurrentUserService currentUserService,
		IIdentityService identityService)
	{ 
		_timer = new Stopwatch();
		_logger = logger; 
		_currentUserService = currentUserService; 
		_identityService = identityService;
	} 
	
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		_timer.Start();
		var response = await next();
		_timer.Stop();
		if (_timer.ElapsedMilliseconds > 500)
		{
			var requestName = typeof(TRequest).Name;
			var userId = _currentUserService.UserId ?? string.Empty;
			var userName = string.Empty;
			if (!string.IsNullOrEmpty(userId))
			{
				userName = await _identityService.GetUserNameAsync(userId);
			}
			_logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
				requestName, _timer.ElapsedMilliseconds, userId, userName, request);
		}
		return response;
	}
}