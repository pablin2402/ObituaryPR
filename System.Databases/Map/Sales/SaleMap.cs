using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Entity.Sales;

namespace System.Databases.Map.Sales
{
    public class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
                builder.ToTable("venta")
                    .HasKey(v => v.idventa);
                builder.HasOne(v => v.persona)
                    .WithMany(p => p.ventas)
                    .HasForeignKey(v => v.idcliente);
        }
        
    }
}
