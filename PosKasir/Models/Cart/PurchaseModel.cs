using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Models.Cart
{
    public class PurchaseModel
    {
        public string TransactionID { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }

    }
}
