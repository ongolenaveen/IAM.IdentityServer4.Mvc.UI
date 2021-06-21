using IdentityServer.Mvc.UI.Services;
using IdentityServer.MVC.UI.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Web;

namespace IdentityServer.Mvc.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
        }
        [HttpGet]
        public IActionResult Index([FromQuery] string returnUrl)
        {
            //var uri = new Uri(returnUrl);
            //var redirectUri = HttpUtility.ParseQueryString(uri.Query).Get("redirect_uri");
            var updatedUrl = returnUrl.Replace("https://localhost:5001", "");
            var loginRequest = new LoginRequest
            {
                ReturnUrl = returnUrl
            };
            return View(loginRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            request.ReturnUrl = request.ReturnUrl.Replace("https://localhost:5001", "");
            request.RememberLogin = true;
            var loginResponse = await _loginService.Login(request);
            
            request.ReturnUrl = "https://localhost:5001" + request.ReturnUrl;
            return Redirect(request.ReturnUrl);
        }
    }
}