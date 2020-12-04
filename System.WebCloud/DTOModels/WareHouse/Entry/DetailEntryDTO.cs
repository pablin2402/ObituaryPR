
using System.ComponentModel.DataAnnotations;

namespace System.WebCloud.DTOModels.WareHouse.Entry
{
    public class DetailEntryDTO
    {
        [Required]
        public int idarticulo { get; set; }

        public string articulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }
    }
}
