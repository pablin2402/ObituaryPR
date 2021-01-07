
using System.ComponentModel.DataAnnotations;
using System.Entity.WareHouse;

namespace System.Entity.Sales
{
    public class SalesDetails
    {
        [Key]

        public int iddetalle_venta { get; set; }
        [Required]
        public int idventa { get; set; }
        [Required]
        public int idarticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }
        [Required]
        public decimal descuento { get; set; }

        public Sale venta { get; set; }
        public Article articulo { get; set; }
    }
}
