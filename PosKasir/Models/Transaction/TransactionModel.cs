using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Models.Transaction
{
    /// <summary>
    /// model untuk transactionheader
    /// </summary>
    public class TransactionModel
    {
        public string TransactionId { get; set; }
        public string UserName { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
