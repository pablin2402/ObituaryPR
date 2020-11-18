using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Entity.Users;
using System.Text;

namespace System.Databases.Map.Users
{
    public class MapCondolence : IEntityTypeConfiguration<Condolence>
    {
        public void Configure(EntityTypeBuilder<Condolence> builder)
        {
            builder.ToTable("condolencias")
               .HasKey(u => u.idcondolencia);
        }
    }
}
