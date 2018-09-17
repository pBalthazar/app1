using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SondageServer.Services
{
    public static class CustomAuthenticationExtensions
    {
        public static AuthenticationBuilder AddCustomAuthentication(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<CustomAuthenticationOptions> configureOptions)
        {
            return builder.AddScheme<CustomAuthenticationOptions, CustomAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
