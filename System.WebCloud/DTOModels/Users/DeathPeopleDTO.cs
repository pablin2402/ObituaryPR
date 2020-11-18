using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System.WebCloud.DTOModels.Users
{
    public class DeathPeopleDTO
    {
        public int idmuerto { get; set; }
        
        public int idusuario { get; set; }
        public string usuario { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public DateTime fecha_muerte { get; set; }
        public string lugar_nacimiento { get; set; }
        public string imagen1 { get; set; }
        public string imagen2 { get; set; }
        public string imagen3 { get; set; }
        public string imagen4 { get; set; }
        public string imagen5 { get; set; }
        public string descripcion { get; set; }
        public string musica { get; set; }
        public string genero { get; set; }
        public string designacion { get; set; }
        public bool condicion { get; set; }
    }
}
