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
    [Authorize(Roles = UserRole.User + ", " + UserRole.Admin)]
    public class IndexModel : PageModel
    {
        private readonly AppPosServices _appposman;
        private readonly IDataProtector _protector;

        public List<PurchaseCartModel> purchaseCartModels { get; set; }

        public PurchaseModel order { get; set; }
        public PurchaseDetailModel detailorder { get; set; }
        public string ProtectedData { get; set; }

        [TempData]
        public string SuccessMessage { set; get; }

        public IndexModel(AppPosServices appPosServices, IDataProtectionProvider protectionProvider)
        {
            this._appposman = appPosServices;
            _protector = protectionProvider.CreateProtector("MyIndex");
        }


        public string change(int id)
        {
            ProtectedData = _protector.Protect(id.ToString());
            return ProtectedData;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var Username = User.FindFirst(ClaimTypes.NameIdentifier).Value;//dapatin nama yg login
            var DataLogIn = await this._appposman.GetLogin(Username);//dapetin data berasarkan username

            await this._appposman.FetcAllById(DataLogIn.UserId);
            this.purchaseCartModels = this._appposman.Purchases;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var usernamelogin = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var DataLogIn = await this._appposman.GetLogin(usernamelogin);

            await this._appposman.FetcAllById(DataLogIn.UserId);
            this.purchaseCartModels = this._appposman.Purchases;

            string firstname = usernamelogin.Substring(0, 2);
            DateTimeOffset moment = DateTimeOffset.Now;
            var datetimeformat = moment.ToString("yyyyMMddTHHmmss");

            string transactionid = firstname + datetimeformat ;

            order = new PurchaseModel()
            {
                TransactionID = transactionid,
                UserId = DataLogIn.UserId,
                PurchaseDate = DateTime.Now
            };
            await this._appposman.InsertTransactionHeader(order);

            foreach (var item in this.purchaseCartModels)
            {
                detailorder = new PurchaseDetailModel()
                {
                    UserId = item.UserId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TransactionId = transactionid
                };

                var result = await this._appposman.InsertransactionDetail(detailorder);
                if (result == false)
                {
                    SuccessMessage = "failed";
                    return Page();
                }

            };
            await this._appposman.SaveToDatabase();
            SuccessMessage = "Thank You for your Order\n" +"Your Transaction ID : " + transactionid;
            purchaseCartModels = new List<PurchaseCartModel>();
            return Page();

        }
    }
}
