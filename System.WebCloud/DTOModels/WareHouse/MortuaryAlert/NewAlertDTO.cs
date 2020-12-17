using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebCloud.DTOModels.WareHouse.MortuaryAlert
{
    public class NewAlertDTO
    {
       public string nombre { get; set; }
       public string apellido { get; set; }
       public string correo { get; set; }
       public int telefono { get; set; }
       public string mensaje { get; set; }
       public int idempresa { get; set; }
    }
}
