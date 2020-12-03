using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosKasir.Enums;
using PosKasir.Models.Cart;
using PosKasir.Services;

namespace PosKasir.Pages.Cart
{
    [Authorize(Roles = UserRole.Admin + ", " + UserRole.User)]
    public class UpdateModel : PageModel
    {
        private readonly AppPosServices _appposman;
        private readonly IDataProtector _protector;

        public UpdateModel(AppPosServices appPosServices, IDataProtectionProvider dataProtection)
        {
            this._appposman = appPosServices;
            this._protector = dataProtection.CreateProtector("MyIndex");
        }

        [BindProperty]
        public PurchaseFormModel Form { set; get; }
        public int IdProduct {set;get;}


        public async Task<IActionResult> OnGetAsync(string id)
        {
            var UserNameLogin = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var UserIdLogin = (await this._appposman.GetLogin(UserNameLogin)).UserId;
            IdProduct = Int32.Parse(_protector.Unprotect(id));

            Form = await _appposman.GetPurchaseByProductId(IdProduct, UserIdLogin);
            if (Form == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (Form.Quantity <= 0)
            {
                //successmessage = "Quantity cant be empty!!";
                return RedirectToPage();
            }
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var userNameLogin = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userIdLogin = (await this._appposman.GetLogin(userNameLogin)).UserId;
            IdProduct = Int32.Parse(_protector.Unprotect(id));
            Form = new PurchaseFormModel()
            {
                UserId = userIdLogin,
                ProductId = IdProduct,
                Quantity = Form.Quantity
            };
            await _appposman.UpdateCart(Form);
            return RedirectToPage("/cart/index");
        }
    }
}
