using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace System.Entity.Users
{
    public class Condolence
    {
        [Key]
        public int idcondolencia { get; set; }
        [Required]
        [ForeignKey("usuario")]
        public int idusuario { get; set; }
        [ForeignKey("fallecido")]
        public int idmuerto { get; set; }  
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string titulo { get; set; }
        public string mensaje { get; set; }
        public DateTime fecha { get; set; }
      
        public bool condicion { get; set; }
        public User usuario { get; set; }
        public DeathPeople fallecido { get; set; }
    }
}
