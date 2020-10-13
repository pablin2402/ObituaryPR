using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Entity.Users;

namespace System.Databases.Map.Users
{
    public class MapRol : IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("rol")
               .HasKey(r => r.idrol);
        }
    }
}
