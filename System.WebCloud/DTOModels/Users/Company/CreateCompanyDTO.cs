using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebCloud.DTOModels.Users.Company
{
    public class CreateCompanyDTO
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }


        public int idusuario { get; set; }
 
        public int idcategoria { get; set; }
   
    }
}
