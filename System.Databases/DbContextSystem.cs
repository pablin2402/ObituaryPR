using Microsoft.EntityFrameworkCore;
using System.Database.Map.WareHouse;
using System.Databases.Map.Sales;
using System.Databases.Map.Users;
using System.Databases.Map.WareHouse;
using System.Entity.Sales;
using System.Entity.Users;
using System.Entity.WareHouse;

namespace System.Database
{
   //comentario
    public class DbContextSystem : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<IncomeDetail> IncomeDetails { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public DbSet<SalesDetails> DetailsSales { get; set; }
        public DbSet<Mortuary> Mortuaries { get; set; }
        public DbSet<DeathPeople> DeathPeoples { get; set; }
        public DbSet<Condolence> Condolences { get; set; }


        public DbContextSystem(DbContextOptions<DbContextSystem> options) : base(options) 
        { 
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MapCategory());
            modelBuilder.ApplyConfiguration(new MapArticle());
            modelBuilder.ApplyConfiguration(new MapRol());
            modelBuilder.ApplyConfiguration(new MapUser());
            modelBuilder.ApplyConfiguration(new MapPerson());
            modelBuilder.ApplyConfiguration(new IncomeDetailMap());
            modelBuilder.ApplyConfiguration(new MapEntry());
            modelBuilder.ApplyConfiguration(new SaleMap());
            modelBuilder.ApplyConfiguration(new DetailSaleMap());
            modelBuilder.ApplyConfiguration(new MapMortuary());
            modelBuilder.ApplyConfiguration(new MapFallecido());
            modelBuilder.ApplyConfiguration(new MapCondolence());





        }
    }
}
