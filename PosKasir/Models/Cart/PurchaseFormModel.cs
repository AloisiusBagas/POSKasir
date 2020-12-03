using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Models.Cart
{
    public class PurchaseFormModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name ="ProductId")]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime PurchaseDate { get; set; }
        public string TransactionID { get; set; }
    }
}
