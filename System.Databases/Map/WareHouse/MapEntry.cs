
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Entity.WareHouse;

namespace System.Databases.Map.WareHouse
{
    public class MapEntry : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.ToTable("ingreso")
                .HasKey(i => i.idingreso);
            builder.HasOne(i => i.persona)
                .WithMany(p => p.ingresos)
                .HasForeignKey(i => i.idproveedor);
        }
    }
}
