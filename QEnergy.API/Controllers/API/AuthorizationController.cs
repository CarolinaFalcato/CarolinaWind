using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QEnergy.API.Controllers.Base;
using QEnergy.API.Utils;
using QEnergy.Core.Actions.Users;
using QEnergy.Core.Domain.Configuration;
using QEnergy.Core.Domain.Models.Authorization;
using System.Diagnostics;
using System.Net;
using System.Net.Mime;

namespace QEnergy.API.Controllers.API
{
    [Route("api/v1/authorization")]
    public class AuthorizationController : ApiControllerBase
    {
        private readonly JwtAuthorization _configuration;

        public AuthorizationController(IMediator mediator, IOptions<JwtAuthorization> configuration, ILogger<AuthorizationController> logger)
            : base(mediator, logger)
        {
            _configuration = configuration.Value;
        }

        /// <summary>
        /// Get JWT authorization token 
        /// </summary>
        /// <remarks>
        /// Get JWT authorization token by providing the client ApiKey and ApiSecret. If successful, a JWT authorization token will be retrieved
        /// </remarks>
        /// <param name="authorizationModel">The client data (ApiKey and ApiSecret)</param>
        /// <returns>JWT authorization token</returns>
        /// <response code="200">Returns a JWT authorization token</response>   
        /// <response code="400">Invalid client credentials</response> 
        /// <response code="500">Internal Server Error</response>         
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Authorization([FromBody] AuthorizationModel authorizationModel)
        {
            if (authorizationModel == null)
                return BadRequest("Invalid POST data");
            if (!IsValid(authorizationModel))
                return BadRequest("Invalid parameters");
            try
            {
                var userAuthorizationModel = await TryCatch(() => _mediator.Send(new ValidateLogin(authorizationModel.ApiKey, authorizationModel.ApiSecret)));
                if (userAuthorizationModel == null)
                    return BadRequest("Invalid credentials");
                else
                {
                    var token = JwtTokenUtil.GenerateJWTToken(userAuthorizationModel, _configuration, _logger);
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Error in {0}. Error: {1}", new StackTrace().GetFrame(0).GetMethod().Name, ex.ToString()));
                return StatusCode(500);
            }
        }

        private bool IsValid(AuthorizationModel authorizationModel)
        {
            return !string.IsNullOrEmpty(authorizationModel?.ApiKey?.Trim()) && !string.IsNullOrEmpty(authorizationModel?.ApiSecret?.Trim());
        }
    }
}
