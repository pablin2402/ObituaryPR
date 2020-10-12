using System.Collections.Generic;

namespace System.Entity.WareHouse
{
    public class Category
    {
        public int idcategoria { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
        public ICollection<Article> articulos { get; set; }
    }
}
