using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Entity.Sales;
using System.Entity.Users;

namespace System.Entity.WareHouse
{
    public class Entry
    {
        [Key]
        public int idingreso { get; set; }
        [Required]
        public int idproveedor { get; set; }
        [Required]
        [ForeignKey("usuario")]
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

        public ICollection<IncomeDetail> detalles { get; set; }
        public User usuario { get; set; }
        public Person persona { get; set; }
    
    }
}
