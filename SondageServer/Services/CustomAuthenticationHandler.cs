using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace SondageServer.Services
{
    internal class CustomAuthenticationHandler :
        AuthenticationHandler<CustomAuthenticationOptions>
    {
        readonly string API_KEY = "A2D3-HTDG-MLU2-3AM5";

        public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) :
            base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                if (this.Request.Headers["Authorization"] == API_KEY)
                {
                    return AuthenticateResult.Success(
                        new AuthenticationTicket(
                            new ClaimsPrincipal(
                            new ClaimsIdentity("Custom Scheme")),
                    Scheme.Name));
                }
                else {
                    return AuthenticateResult.Fail("Unauthorized Access");
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Error message.");
            }
        }
    }
}
