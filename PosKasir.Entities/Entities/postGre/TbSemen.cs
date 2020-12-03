using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosKasir.Entities.Entities.postGre
{
    public partial class TbSemen
    {
        public TbSemen()
        {
            TbTransactionDetail = new HashSet<TbTransactionDetail>();
        }

        [Key]
        public int SemenId { get; set; }
        [Required]
        [StringLength(255)]
        public string SemenName { get; set; }
        [Column(TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }
        public int Stock { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime CreatedAt { get; set; }
        [Required]
        [StringLength(255)]
        public string CreatedBy { get; set; }

        [InverseProperty("Semen")]
        public virtual ICollection<TbTransactionDetail> TbTransactionDetail { get; set; }
    }
}
