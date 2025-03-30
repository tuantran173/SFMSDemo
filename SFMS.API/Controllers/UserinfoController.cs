﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using SFMSSolution.Domain.Entities;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SFMSSolution.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserinfoController(UserManager<User> userManager) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;

        //
        // GET: /api/userinfo
        [Authorize(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
        [HttpGet("~/connect/userinfo"), HttpPost("~/connect/userinfo"), Produces("application/json")]
        public async Task<IActionResult> Userinfo()
        {
            var Id = User.FindFirst(Claims.Subject)?.Value;
            var user = await _userManager.FindByIdAsync(Id!);
            if (user == null)
            {
                return Challenge(
                    authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                    properties: new AuthenticationProperties(new Dictionary<string, string?>
                    {
                        [OpenIddictServerAspNetCoreConstants.Properties.Error] = Errors.InvalidToken,
                        [OpenIddictServerAspNetCoreConstants.Properties.ErrorDescription] =
                            "The specified access token is bound to an account that no longer exists."
                    }));
            }

            var claims = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                // Note: the "sub" claim is a mandatory claim and must be included in the JSON response.
                [Claims.Subject] = (await _userManager.GetUserIdAsync(user))!
            };

            if (User.HasScope(Scopes.Email))
            {
                claims[Claims.Email] = (await _userManager.GetEmailAsync(user))!;
                claims[Claims.EmailVerified] = (await _userManager.IsEmailConfirmedAsync(user))!;
            }

            if (User.HasScope(Scopes.Phone))
            {
                claims[Claims.PhoneNumber] = (await _userManager.GetPhoneNumberAsync(user))!;
                claims[Claims.PhoneNumberVerified] = (await _userManager.IsPhoneNumberConfirmedAsync(user))!;
            }

            if (User.HasScope(Scopes.Roles))
            {
                claims[Claims.Role] = (await _userManager.GetRolesAsync(user))!;
            }

            claims[Claims.Name] = (await _userManager.GetUserNameAsync(user))!;


            // Note: the complete list of standard claims supported by the OpenID Connect specification
            // can be found here: http://openid.net/specs/openid-connect-core-1_0.html#StandardClaims

            return Ok(claims);
        }
    }
}
