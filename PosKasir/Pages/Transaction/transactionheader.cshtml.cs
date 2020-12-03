using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PosKasir.Enums;

namespace PosKasir.Pages.Transaction
{
    [Authorize(Roles = UserRole.Admin)]
    public class transactionheaderModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
