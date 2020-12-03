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
    [Authorize(Roles = UserRole.Admin + ", " +UserRole.User)]
    public class DeleteModel : PageModel
    {
        private readonly AppPosServices _appposman;
        private readonly IDataProtector _protector;

        public DeleteModel(AppPosServices appPosServices, IDataProtectionProvider protectionProvider)
        {
            this._appposman = appPosServices;
            _protector = protectionProvider.CreateProtector("MyIndex");
        }

        [BindProperty]
        public PurchaseFormModel Forms { get; set; }
        [BindProperty]
        public int IdProduct { set; get; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var userNameLogin = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userIdLogin = (await this._appposman.GetLogin(userNameLogin)).UserId;
            IdProduct = Int32.Parse(_protector.Unprotect(id));
            Forms = await _appposman.GetPurchaseByProductId(IdProduct, userIdLogin);

            if (Forms == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            var userNameLogin = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userIdLogin = (await this._appposman.GetLogin(userNameLogin)).UserId;
            IdProduct = Int32.Parse(_protector.Unprotect(id));
            Forms = await _appposman.GetPurchaseByProductId(IdProduct, userIdLogin);

            await _appposman.DeleteCart(Forms.ProductId,Forms.UserId);
            return RedirectToPage("/cart/index");
        }
    }
}
