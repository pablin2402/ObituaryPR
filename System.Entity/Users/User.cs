using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Entity.Sales;
using System.Entity.WareHouse;

namespace System.Entity.Users
{
    public class User
    {
        public int idusuario { get; set; }
        [ForeignKey("rol")]
        public int idrol { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public byte[] password_hash { get; set; }
        [Required]
        public byte[] password_salt { get; set; }
        public bool condicion { get; set; }

        public Rol rol { get; set; }
        public ICollection<Entry> detalles { get; set; }
        public ICollection<Sale> ventas { get; set; }



    }
}
