using JsonAdventure.Api.Constants;
using JsonAdventure.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace JsonAdventure.Api.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly string AuthHeaderName = "Authorization";
        private readonly IUserService _userService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserService userService)
            : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }

        private async Task<AuthenticateResult> SetupErrorResponse(int statusCode, string message)
        {
            var errorResponse = new ProblemDetails
            {
                Status = statusCode,
                Title = message
            };
            Response.StatusCode = statusCode;
            Response.Headers.Authorization = "Basic";
            await Response.WriteAsJsonAsync(errorResponse);

            return AuthenticateResult.Fail(message);
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthHeaderName))
            {
                return await SetupErrorResponse(
                    StatusCodes.Status401Unauthorized,
                    AuthenticationErrorMessages.MissingHeader);
            }

            string username, password;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[AuthHeaderName]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter!);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                username = credentials[0];
                password = credentials[1];
            }
            catch
            {
                return await SetupErrorResponse(
                    StatusCodes.Status400BadRequest,
                    AuthenticationErrorMessages.InvalidHeader);
            }

            var foundUser = _userService.GetUser(username);
            if (username != foundUser.Name || password != foundUser.Password)
            {
                return await SetupErrorResponse(
                    StatusCodes.Status400BadRequest,
                    AuthenticationErrorMessages.InvalidCredentials);
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()),
                new Claim(ClaimTypes.Name, foundUser.Name),
                new Claim(ClaimTypes.Role, foundUser.Role.ToString()),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}