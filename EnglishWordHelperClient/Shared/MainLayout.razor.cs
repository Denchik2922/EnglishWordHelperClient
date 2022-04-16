using EnglishWordHelperClient.HttpServices.Interfaces;
using EnglishWordHelperClient.Infrastructure;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace EnglishWordHelperClient.Shared
{
    public partial class MainLayout : IDisposable
    {

        [Inject]
        private HttpInterceptorService _interceptor { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _interceptor.RegisterEvent();
        }

        [Inject]
        private IAuthHttpService AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        public async Task LogoutUser()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/login");
        }

        public void Dispose() => _interceptor.DisposeEvent();
    }
}
