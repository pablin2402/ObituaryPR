using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebCloud.DTOModels.Users.Condolence
{
    public class CreateCondolenceDTO
    {
        public int idusuario { get; set; }

        public int idmuerto { get; set; }
        public string titulo { get; set; }
        public string mensaje { get; set; }
        public DateTime fecha { get; set; }

    }
}
