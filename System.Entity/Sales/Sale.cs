using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Entity.Users;

namespace System.Entity.Sales
{
    public class Sale
    {
        [Key]

        public int idventa { get; set; }
        [Required]
        [ForeignKey("usuario")]

        public int idcliente { get; set; }
        [Required]

        public int idusuario { get; set; }
        [Required]
        public string tipo_comprobante { get; set; }
        public string serie_comprobante { get; set; }
        [Required]
        public string num_comprobante { get; set; }
        [Required]
        public DateTime fecha_hora { get; set; }
        [Required]
        public decimal impuesto { get; set; }
        [Required]
        public decimal total { get; set; }
        [Required]
        public string estado { get; set; }

        public ICollection<SalesDetails> detalles { get; set; }
        public User usuario { get; set; }
        public Person persona { get; set; }
    }
}
