
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Entity.WareHouse;

namespace System.Databases.Map.WareHouse
{
    public class IncomeDetailMap : IEntityTypeConfiguration<IncomeDetail>
    {
        public void Configure(EntityTypeBuilder<IncomeDetail> builder)
        {
            builder.ToTable("detalle_ingreso")
                 .HasKey(d => d.iddetalle_ingreso);
        }
    }
}
