using Microsoft.EntityFrameworkCore;
using System.Database.Map.WareHouse;
using System.Entities.WareHouse;

namespace System.Database
{
   //comentario
    public class DbContextSystem : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbContextSystem(DbContextOptions<DbContextSystem> options) : base(options) 
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MapCategory());

        }
    }
}
