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
        public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) :
            base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                
                return AuthenticateResult.Success(
                    new AuthenticationTicket(
                        new ClaimsPrincipal(),
                        new AuthenticationProperties(),
                        Scheme.Name));
            }
            catch
            {
                return AuthenticateResult.Fail("Error message.");
            }
        }
    }
}
