using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosKasir.Entities.Entities.postGre
{
    public partial class TbUser
    {
        public TbUser()
        {
            TbTransaction = new HashSet<TbTransaction>();
            TbTransactionDetail = new HashSet<TbTransactionDetail>();
        }

        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(255)]
        public string UserName { get; set; }
        [Required]
        [StringLength(255)]
        public string UserPassword { get; set; }
        [Required]
        [StringLength(255)]
        public string UserRole { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<TbTransaction> TbTransaction { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<TbTransactionDetail> TbTransactionDetail { get; set; }
    }
}
