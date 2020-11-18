using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace System.Entity.WareHouse
{
    public class Mortuary
    {
        [Key]
        public int idfuneraria { get; set; }
        [ForeignKey("categoria")]
        public int idcategoria { get; set; }
        public string codigo { get; set; }
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "El nombre no debe de tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        [Required]
        public decimal telefono { get; set; }
        
        public string descripcion { get; set; }
        public string servicios { get; set; }
        public string imagen { get; set; }
        public string imagen2 { get; set; }


        public decimal posicionx { get; set; }
        public decimal posiciony { get; set; }

        public bool condicion { get; set; }
        public string direccion { get; set; }


        public Category  categoria { get; set; }
        //public ICollection<IncomeDetail> DetallesIngreso { get; set; }
    }
}
