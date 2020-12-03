using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Models.UserTransaction
{
    public class TransactionUserModel
    {
        public string TransactionId { get; set; }
        public string ProductName { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int quantity { get; set; }
    }
}
