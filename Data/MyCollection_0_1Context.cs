using Microsoft.EntityFrameworkCore;
using MyCollection_0._1.Model;

namespace MyCollection_0._1.Data
{
    public class MyCollection_0_1Context : DbContext
    {
        public MyCollection_0_1Context(DbContextOptions<MyCollection_0_1Context> options)
            : base(options)
        {
        }

      
        public DbSet<Item> Item { get; set; } = default!;

        
        public DbSet<FavoriteItem> FavoriteItems { get; set; } = default!;

        
        public DbSet<History> Histories { get; set; } = default!; 

        // Méthode de configuration 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Item>()
                .HasKey(i => i.Id); 

            modelBuilder.Entity<Item>()
                .Property(i => i.Id)
                .ValueGeneratedOnAdd(); 

           
            modelBuilder.Entity<FavoriteItem>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<FavoriteItem>()
                .HasOne(f => f.Item)
                .WithMany()
                .HasForeignKey(f => f.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<History>()
                .Property(h => h.Timestamp)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
