using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Context
{
    public class ServiceContext:DbContext
    {
        public ServiceContext(DbContextOptions<ServiceContext> options):base(options)
        {
            
        }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Product> Products { get; set; }    
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Enrollment>().HasOne<Product>(e=>e.Product).WithMany(p=>p.Enrollments)
            //    .HasForeignKey(e=>e.ProductID).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            //modelBuilder.Entity<Enrollment>().HasOne<Category>(e=>e.Category).WithMany(p => p.Enrollments)
            //   .HasForeignKey(e => e.CategoryID).IsRequired(false).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Product>().HasMany<Enrollment>(p => p.Enrollments).
                WithOne(e => e.Product).HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasMany<Enrollment>(p => p.Enrollments).
                WithOne(e => e.Category).HasPrincipalKey(c => c.Id).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
