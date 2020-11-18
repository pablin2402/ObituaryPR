
namespace System.WebCloud.DTOModels.WareHouse.Mortuary
{
    public class MortuaryDTO
    {
        public int idfuneraria { get; set; }
        public int idcategoria { get; set; }
        public string categoria { get; set; }
        public string codigo { get; set; }
 
        public string nombre { get; set; }
        public decimal telefono { get; set; }

        public string descripcion { get; set; }
        public string servicios { get; set; }
        public string imagen { get; set; }
        public string imagen2 { get; set; }


        public decimal posicionx { get; set; }
        public decimal posiciony { get; set; }

        public bool condicion { get; set; }
        public string direccion { get; set; }


    }
}
