using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Entity.Users;
using System.Text;

namespace System.Databases.Map.Users
{
    public class MapFallecido : IEntityTypeConfiguration<DeathPeople>
    {
        public void Configure(EntityTypeBuilder<DeathPeople> builder)
        {
            builder.ToTable("fallecido")
               .HasKey(u => u.idmuerto);
        }
    }
}
