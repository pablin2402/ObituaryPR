using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Entity.WareHouse;
using System.Text;

namespace System.Databases.Map.WareHouse
{
    public class MapFuneraryAlert : IEntityTypeConfiguration<FuneraryAlert>
    {
     
            public void Configure(EntityTypeBuilder<FuneraryAlert> builder)
            {
                builder.ToTable("funeraryalert")
                        .HasKey(a => a.id_alert);
            }
        
    }
}
