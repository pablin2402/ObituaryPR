using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Entity.Users;
using System.Text;

namespace System.Databases.Map.Users
{
    public class MapCompany : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("empresa")
               .HasKey(u => u.idempresa);
        }
    }
}
