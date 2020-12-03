using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosKasir.Entities.Entities.postGre
{
    public partial class TbTransactionDetail
    {
        [Key]
        public int TransactionDetailId { get; set; }
        [Required]
        [Column("TransactionID")]
        [StringLength(20)]
        public string TransactionId { get; set; }
        public int UserId { get; set; }
        public int SemenId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [ForeignKey(nameof(SemenId))]
        [InverseProperty(nameof(TbSemen.TbTransactionDetail))]
        public virtual TbSemen Semen { get; set; }
        [ForeignKey(nameof(TransactionId))]
        [InverseProperty(nameof(TbTransaction.TbTransactionDetail))]
        public virtual TbTransaction Transaction { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TbUser.TbTransactionDetail))]
        public virtual TbUser User { get; set; }
    }
}
