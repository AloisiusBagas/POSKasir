using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosKasir.Entities.Entities.postGre
{
    public partial class TbTransaction
    {
        public TbTransaction()
        {
            TbTransactionDetail = new HashSet<TbTransactionDetail>();
        }

        [Key]
        [Column("TransactionID")]
        [StringLength(20)]
        public string TransactionId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime TransacionDate { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TbUser.TbTransaction))]
        public virtual TbUser User { get; set; }
        [InverseProperty("Transaction")]
        public virtual ICollection<TbTransactionDetail> TbTransactionDetail { get; set; }
    }
}
