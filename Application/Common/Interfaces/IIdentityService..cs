﻿namespace DoMe.Application.Common.Interfaces;

public interface IIdentityService
{
	Task<string?> GetUserNameAsync(string userId);
}