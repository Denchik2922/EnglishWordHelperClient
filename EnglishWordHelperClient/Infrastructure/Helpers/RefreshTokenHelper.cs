﻿using EnglishWordHelperClient.HttpServices.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;

namespace EnglishWordHelperClient.Infrastructure.Helpers
{
    public class RefreshTokenHelper
    {
        private readonly AuthenticationStateProvider _authProvider;
        private readonly IAuthHttpService _authService;
        public RefreshTokenHelper(AuthenticationStateProvider authProvider, IAuthHttpService authService)
        {
            _authProvider = authProvider;
            _authService = authService;
        }
        public async Task<string> TryRefreshToken()
        {
            var authState = await _authProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            var exp = user.FindFirst(c => c.Type.Equals("exp")).Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(exp));
            var timeUTC = DateTime.UtcNow;
            var diff = expTime - timeUTC;
            if (diff.TotalMinutes <= 2)
                return await _authService.RefreshToken();
            return string.Empty;
        }
    }
}
