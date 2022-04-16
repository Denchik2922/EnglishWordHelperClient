using EnglishWordHelperClient.HttpServices.Interfaces;
using EnglishWordHelperClient.Models;
using Microsoft.AspNetCore.Components;

namespace EnglishWordHelperClient.Pages.AuthPages
{
    public partial class Login
    {
        private UserLogin _loginModel = new UserLogin();

        [Inject]
        private IAuthHttpService AuthService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private async void LoginUser()
        {
            var result = await AuthService.Login(_loginModel);
            if (result)
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}
