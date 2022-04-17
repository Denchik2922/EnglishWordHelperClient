using Blazored.LocalStorage;
using EnglishWordHelperClient.HttpServices;
using EnglishWordHelperClient.HttpServices.Interfaces;
using EnglishWordHelperClient.Infrastructure;
using EnglishWordHelperClient.Infrastructure.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace EnglishWordHelperClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/api/")
            }
            .EnableIntercept(sp));

            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddScoped<HttpInterceptorService>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);

            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<RefreshTokenHelper>();

            builder.Services.AddScoped<IAuthHttpService, AuthHttpService>();
            builder.Services.AddScoped<IWordHttpService, WordHttpService>();

            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            await builder.Build().RunAsync();
        }
    }
}
