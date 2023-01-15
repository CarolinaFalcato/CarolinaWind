using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QEnergy.API.Controllers.Base;
using QEnergy.API.Models.Login;
using QEnergy.API.Utils;
using QEnergy.Core.Actions.Users;
using QEnergy.Core.Domain.Configuration;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QEnergy.API.Controllers
{
    public class LoginController : MVCControllerBase
    {
        private readonly JwtAuthorization _configuration;

        public LoginController(IOptions<JwtAuthorization> configuration, IMediator mediator, ILogger<LoginController> logger) : base(mediator, logger)
        {
            _configuration = configuration.Value;
        }

        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel, string returnUrl = null)
        {
            ViewBag.returnUrl = returnUrl == "/" ? null : returnUrl;
            var returnTo = "/Projects/List";
            try
            {
                var userModel = await TryCatch(() => _mediator.Send(new ValidateLogin(loginModel.Username, loginModel.Password)));
                if (userModel == null)
                {
                    ModelState.AddModelError("", TextResources.TextResources.Login_InvalidCredentials);
                    return View(loginModel);
                }
                else
                {
                    var token = JwtTokenUtil.GenerateJWTToken(userModel, _configuration, _logger);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GetClaimsPrincipal(token));

                    if (!string.IsNullOrEmpty(returnUrl))
                        returnTo = returnUrl;
                    return Redirect(returnTo);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}. Error: {1}", new StackTrace().GetFrame(0).GetMethod().Name, ex.ToString()));
                return StatusCode(500);
            }
        }


        public ClaimsPrincipal GetClaimsPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                var key = Encoding.ASCII.GetBytes(_configuration.SecretKey);
                TokenValidationParameters parameters = new()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    RequireAudience = true,
                    ValidateAudience = true,
                    ValidAudience = _configuration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                };
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken securityToken);
                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
                principal.Identities.First().AddClaim(new Claim("access_token", jwtToken.RawData));
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
