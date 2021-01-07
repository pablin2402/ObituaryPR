using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Entity.Sales;

namespace System.Databases.Map.Sales
{
    public class MapPerson : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("persona")
               .HasKey(p => p.idpersona);
        }
    }
}
