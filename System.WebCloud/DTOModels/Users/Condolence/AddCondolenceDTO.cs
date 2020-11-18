
namespace System.WebCloud.DTOModels.Users.Condolence
{
    public class AddCondolenceDTO { 
        public int idcondolencia { get; set; }
        public int idusuario { get; set; }
        public string usuario { get; set; }
        public string fallecido { get; set; }

        public int idmuerto { get; set; }
        public string titulo { get; set; }
        public string mensaje { get; set; }
        public DateTime fecha { get; set; }

        public bool condicion { get; set; }
    }
}
