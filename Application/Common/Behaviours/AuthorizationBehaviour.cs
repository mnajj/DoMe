using System.Reflection;
using DoMe.Application.Common.Exceptions;
using DoMe.Application.Common.Interfaces;
using DoMe.Application.Common.Security;
using MediatR;

namespace DoMe.Application.Common.Behaviours;

public sealed class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
	private readonly ICurrentUserService _currentUserService;
	private readonly IIdentityService _identityService;

	public AuthorizationBehaviour(ICurrentUserService currentUserService, IIdentityService identityService)
		=> (_currentUserService, _identityService) = (currentUserService, identityService);
	
	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		var authAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
		if (authAttributes.Any())
		{
			if (_currentUserService.UserId is null) throw new UnauthorizedAccessException();

			var attributesWithRoles = authAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));
			if (attributesWithRoles.Any())
			{
				bool isAuthorized = false;
				foreach (var roles in authAttributes.Select(a=>a.Roles.Split(',')))
				{
					foreach (var role in roles)
					{
						if (await _identityService.IsInRoleAsync(_currentUserService.UserId, role.Trim()))
						{
							isAuthorized = true;
							break;
						}
					}

					if (!isAuthorized) throw new ForbiddenAccessException();
				}
			}

			var attributesWithPolicies = authAttributes
				.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
			if (attributesWithPolicies.Any())
			{
				foreach (var policy in attributesWithPolicies.Select(a=>a.Policy))
				{
					var isAuthorized = false;
					if (!await _identityService.AuthorizeAsync(_currentUserService.UserId, policy))
					{
						throw new ForbiddenAccessException();
					}
				}
			}
		}

		return await next();
	}
}