using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebCloud.DTOModels.Users
{
    public class RolDTO
    {
        public int idrol { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
    }
}
