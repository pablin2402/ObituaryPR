using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Entity.WareHouse;

namespace System.Databases.Map.WareHouse
{
    public class MapArticle: IEntityTypeConfiguration<Article> { 
        public void Configure(EntityTypeBuilder<Article> builder)
        {
          builder.ToTable("articulo")
                    .HasKey(a => a.idarticulo);
        }
        
    }
}
