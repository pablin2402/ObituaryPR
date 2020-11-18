using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Entity.Users
{
    public class DeathPeople
    {
        public int idmuerto { get; set; }
        [Required]
        [ForeignKey("usuario")]
        public int idusuario { get; set; }
        [Required]

        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
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
        public User usuario { get; set; }







    }
}
