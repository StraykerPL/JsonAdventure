using JsonAdventure.Api.Constants;
using JsonAdventure.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
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

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthHeaderName))
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                Response.Headers.Authorization = string.Format("Basic '{0}'", AuthenticationErrorMessages.MissingHeader);

                return Task.FromResult(AuthenticateResult.Fail(AuthenticationErrorMessages.MissingHeader));
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
                Response.StatusCode = StatusCodes.Status400BadRequest;
                Response.Headers.Authorization = string.Format("Basic '{0}'", AuthenticationErrorMessages.InvalidHeader);

                return Task.FromResult(AuthenticateResult.Fail(AuthenticationErrorMessages.InvalidHeader));
            }

            var foundUser = _userService.GetUser(username);
            if (username != foundUser.Name || password != "123")
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                Response.Headers.Authorization = string.Format("Basic '{0}'", AuthenticationErrorMessages.InvalidCredentials);

                return Task.FromResult(AuthenticateResult.Fail(AuthenticationErrorMessages.InvalidCredentials));
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, foundUser.Id.ToString()),
                new Claim(ClaimTypes.Name, foundUser.Name),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}