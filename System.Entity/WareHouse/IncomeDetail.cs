
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace System.Entity.WareHouse
{
    public class IncomeDetail
    {
        public int iddetalle_ingreso { get; set; }
        [Required]
       [ForeignKey("ingreso")]
        public int idingreso { get; set; }
        [Required]


        [ForeignKey("articulo")]

        public int idarticulo { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }

        public string telefono { get; set; }

        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }

        public Entry ingreso { get; set; }
        public Article articulo { get; set; }



    }
}
