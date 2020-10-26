using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Entity.WareHouse;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebCloud.DTOModels.WareHouse.Entry
{
    public class CreateEntryDTO
    {
        //Propiedades maestro
        [Required]
        public int idproveedor { get; set; }
        [Required]
        public int idusuario { get; set; }
        [Required]
        public string tipo_comprobante { get; set; }
        public string serie_comprobante { get; set; }
        [Required]
        public string num_comprobante { get; set; }
        [Required]
        public decimal impuesto { get; set; }
        [Required]
        public decimal total { get; set; }
        //Propiedades detalle
      [Required]
         public List<IncomeDetail> detalles { get; set; }
    }
}
