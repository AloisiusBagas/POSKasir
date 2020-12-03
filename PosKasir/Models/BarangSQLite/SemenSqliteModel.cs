using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Models.BarangSQLite
{
    public class SemenSqliteModel
    {
        /// <summary>
        /// model for sqliteDB
        /// </summary>
        public int SemenID { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        [Display(Name = "Semen Name")]
        public string SemenName { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal SemenPrice { get; set; }

        [Required]
        [Display(Name = "Stock")]
        public int SemenStock { set; get; }
    }
}
