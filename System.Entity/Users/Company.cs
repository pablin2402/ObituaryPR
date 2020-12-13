

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Entity.WareHouse;

namespace System.Entity.Users
{
    public class Company
    {
        [Key]
        public int idempresa { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }

        [Required]
        [ForeignKey("usuario")]
        public int idusuario { get; set; }
        [Required]
        [ForeignKey("categoria")]
        public int idcategoria { get; set; }
        public User usuario { get; set; }
        public Category categoria { get; set; }

    }
}
