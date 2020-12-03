using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosKasir.Models.Register;
using PosKasir.Services;

namespace PosKasir.Pages.Register
{
    [AllowAnonymous]
    public class RegisterUserModel : PageModel
    {
        private readonly AppPosServices _AppPosMan;
        public RegisterUserModel(AppPosServices appPosServices)
        {
            this._AppPosMan = appPosServices;
        }
        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string BadMessage { get; set; }
        [BindProperty]
        public RegisterFormModel Form { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            var isUsernameExist = await _AppPosMan.CheckExistUsername(Form.UserName);
            if (isUsernameExist == false)
            {
                BadMessage = "Username already exist";
                return Page();
            }

            var NewUser = await _AppPosMan.InsertUser(Form);
            if (NewUser == false)
            {
                BadMessage = "Register user failed!!";
                return Page();
            }
            else
            {
                SuccessMessage = "Register " + Form.UserName + " Success";
                //await Task.Delay(4000);
            }
            return RedirectToPage("/auth/login");
        }
    }
}
