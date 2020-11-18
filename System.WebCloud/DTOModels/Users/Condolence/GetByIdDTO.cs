using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebCloud.DTOModels.Users.Condolence
{
    public class GetByIdDTO
    {
        public string usuario { get; set; }
        public string fallecido { get; set; }

        public string titulo { get; set; }
        public string mensaje { get; set; }
        public DateTime fecha { get; set; }

        public bool condicion { get; set; }
    }
}
