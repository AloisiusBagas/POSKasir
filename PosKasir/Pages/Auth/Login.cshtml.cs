using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosKasir.Enums;
using PosKasir.Models.Login;
using PosKasir.Services;

namespace PosKasir.Pages.Auth
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly AppPosServices _AppPosMan;
        public LoginModel(AppPosServices AppPosServices)
        {
            this._AppPosMan = AppPosServices;
        }
        [BindProperty]
        public LoginFormModel Forms { get; set; }
        public string RoleDb { get; set; }
        [TempData]
        public string SuccessMessage { get; set; }

        private ClaimsPrincipal GenerateClaims()
        {
            var claims = new ClaimsIdentity(AppPosAuthenticationSchemes.Cookie);
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, Forms.Username));
            claims.AddClaim(new Claim(ClaimTypes.Role, RoleDb));
            return new ClaimsPrincipal(claims);
        }

        public async Task<IActionResult> OnPostAsync(string returnurl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("~/");
            }
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var logindb = await _AppPosMan.GetLogin(Forms.Username);
            if (logindb == null)
            {
                SuccessMessage = "Username has not been registered yet";
                return Page();
            }

            //object = DB
            var Username = logindb.UserName;
            var Password = logindb.UserPassword;
            RoleDb = logindb.UserRole;

            var isUserMatch = Forms.Username == Username;
            var isPassMatch = _AppPosMan.Verify(Forms.Password, Password);

            var isSuccess = isUserMatch && isPassMatch;
            if (isSuccess == false)
            {
                SuccessMessage = "Invalid Username or Password";
                return Page();
            }

            //dapetin username sama rolenya
            var claims = GenerateClaims();
            var persistance = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),
                IsPersistent = true
            };
            await HttpContext.SignInAsync(AppPosAuthenticationSchemes.Cookie, claims, persistance);

            UserEnum.name = Forms.Username;

            if (string.IsNullOrEmpty(returnurl) == false)
            {
                return LocalRedirect(returnurl);
            }
            return Redirect("~/");
        }
    }
}
