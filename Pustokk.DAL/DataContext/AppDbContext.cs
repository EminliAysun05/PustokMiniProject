using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pustokk.DAL.DataContext.Entities;

namespace Pustokk.DAL.DataContext;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> dbContext) : base(dbContext)
    {  
    }

    public DbSet<BasketItem> BasketItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }
    public DbSet<Subscribe> Subscribes { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        //unikal categoryName
        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        //uzunluq productName
        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasMaxLength(3);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)")
            .HasDefaultValue(0);
        // .HasCheckConstraint("CK_Product_Price", "[Price] >= 0");

        //tagName unikal
        modelBuilder.Entity<Tag>()
            .HasIndex(t => t.Name)
            .IsUnique();

        modelBuilder.Entity<BasketItem>()
            .Property(b => b.Quantity)
            .IsRequired()
            .HasDefaultValue(1);
        //check quantity >0

        //email unikal
        modelBuilder.Entity<Subscribe>()
             .HasIndex(s => s.Email)
             .IsUnique();

        //key unik
        modelBuilder.Entity<Setting>()
            .HasIndex(s => s.Key)
            .IsUnique();
    }
}



