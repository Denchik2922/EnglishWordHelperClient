using EnglishWordHelperClient.Infrastructure.Helpers;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace EnglishWordHelperClient.Infrastructure
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navManager;
        private readonly RefreshTokenHelper _refreshTokenService;

        public HttpInterceptorService(HttpClientInterceptor interceptor,
                                      NavigationManager navigationManager,
                                      RefreshTokenHelper refreshTokenService)
        {
            _interceptor = interceptor;
            _navManager = navigationManager;
            _refreshTokenService = refreshTokenService;
        }

        public void RegisterEvent()
        {
            _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
        }
        public async Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            if (!absPath.Contains("auth"))
            {
                var token = await _refreshTokenService.TryRefreshToken();
                if (!string.IsNullOrEmpty(token))
                {
                    e.Request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
                }
            }
        }

        public void DisposeEvent()
        {
            _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
        }
    }
}
