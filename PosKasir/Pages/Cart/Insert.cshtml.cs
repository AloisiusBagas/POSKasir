using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosKasir.Enums;
using PosKasir.Models.Cart;
using PosKasir.Models.Product;
using PosKasir.Services;

namespace PosKasir.Pages.Cart
{
    [Authorize(Roles = UserRole.Admin + ", " + UserRole.User)]
    public class InsertModel : PageModel
    {
        private readonly AppPosServices _appPosman;
        [BindProperty]
        public PurchaseFormModel Form { set; get; }
        [BindProperty]
        public List<ProductViewModel> ListProduct { get; set; }
        [TempData]
        public string successmessage { get; set; }
        public InsertModel(AppPosServices appPos)
        {
            this._appPosman = appPos;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ListProduct = await _appPosman.GetAllProduct();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Form.Quantity <= 0)
            {
                successmessage = "Quantity cant be empty!!";
                return RedirectToPage();
            }
            if (ModelState.IsValid == false)
            {
                return Page();
            }
            var usernamelogin = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var DataLogin = await this._appPosman.GetLogin(usernamelogin);
            Form.UserId = DataLogin.UserId;

            var CheckProductExist = await this._appPosman.CheckProductExists(Form);
            if (CheckProductExist == false)
            {
                successmessage = "Product are already in cart";
                return RedirectToPage();
            }

            var Stock = await _appPosman.CheckStock(Form);

            if (Stock == false)
            {
                successmessage = "Permintaan melebihi Stock, mohon kurangi jumlah pembelian (Quantity) Anda";
                return RedirectToPage();
            }
            else
            {
                await _appPosman.InsertCart(Form);
            }
            return Redirect("/cart");

        }
    }
}
