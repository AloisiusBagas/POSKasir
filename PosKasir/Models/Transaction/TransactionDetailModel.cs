using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Models.Transaction
{
    /// <summary>
    /// model untuk transactiondetail
    /// </summary>
    public class TransactionDetailModel
    {
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string TransactionId { get; set; }
        public int TransactionDetailId { get; set; }

    }
}
