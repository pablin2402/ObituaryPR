using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Entity.Sales;

namespace System.Databases.Map.Sales
{
    public class DetailSaleMap : IEntityTypeConfiguration<SalesDetails>
    {
        public void Configure(EntityTypeBuilder<SalesDetails> builder)
        {
            builder.ToTable("detalle_venta")
                .HasKey(d => d.iddetalle_venta);
        }
    }
}
