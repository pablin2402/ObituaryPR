using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebCloud.DTOModels.Users.Company
{
    public class AddCompanyDTO
    {
        public int idempresa { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
        public int idusuario { get; set; }
        public string usuario { get; set; }
        public int idcategoria { get; set; }
        public string categoria { get; set; }


    }
}
