using IdentityServer.Mvc.UI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;

namespace IdentityServer.Mvc.UI
{
    public static class StartupExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration config)
        {
            //services.AddScoped<ILoginService,LoginService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = cookies,
                UseCookies = true
            };
            services.AddHttpClient<ILoginService, LoginService>().ConfigurePrimaryHttpMessageHandler(() =>
            {
                return handler;
            });
        }
    }
}
