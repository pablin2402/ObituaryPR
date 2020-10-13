
using System.ComponentModel.DataAnnotations;

namespace System.Entity.WareHouse
{
    public class IncomeDetail
    {
        public int iddetalle_ingreso { get; set; }
        [Required]
        public int idingreso { get; set; }
        [Required]
        public int idarticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }

        public Entry ingreso { get; set; }
    }
}
