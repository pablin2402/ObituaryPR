using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Entity.Users;
using System.Text;

namespace System.Entity.WareHouse
{
    public class FuneraryAlert
    {
        [Key]
        public int id_alert { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public int telefono { get; set; }
        public string mensaje { get; set; }
        [ForeignKey("empresa")]
        public int idempresa { get; set; }

        public DateTime fecha { get; set; }

        public Company empresa { get; set; }
    }
}
