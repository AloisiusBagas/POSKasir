using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PosKasir.Entities
{
    public class Semen
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SemenID { get; set; }
        [Required]
        public string SemenName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int stock { get; set; }
        public string CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
