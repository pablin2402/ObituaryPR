using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Entity.WareHouse;

namespace System.Databases.Map.WareHouse
{
    public class MapMortuary : IEntityTypeConfiguration<Mortuary>
    {
        public void Configure(EntityTypeBuilder<Mortuary> builder)
        {
            builder.ToTable("funeraria")
                    .HasKey(a => a.idfuneraria);
        }
    }
}
