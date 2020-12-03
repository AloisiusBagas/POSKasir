using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PosKasir.Models.Barang
{
    /// <summary>
    /// model untuk view semen
    /// </summary>
    public class SemenModel
    {
        public int BarangId { get; set; }
        public string BarangName { get; set; }
        public decimal BarangPrice { get; set; }
        public int BarangStock { get; set; }
    }
}
