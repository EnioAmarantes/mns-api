using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Finance> Finances { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<StockBalance> StockBalances { get; set; }
    public DbSet<StockMove> StockMoves { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }
    public DbSet<Category> Categories { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Company>(entity =>
    {
      entity.ToTable("Companies");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
    });

    modelBuilder.Entity<User>(entity =>
    {
      entity.ToTable("users");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
      entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
      entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(200);
      entity.Property(e => e.MustChangePassword).IsRequired().HasDefaultValue(true);
      entity.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<Category>(entity =>
    {
      entity.ToTable("Categories");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
      entity.HasOne(e => e.Company)
        .WithMany()
        .HasForeignKey(e => e.CompanyId)
        .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<Product>(entity =>
    {
      entity.ToTable("Products");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
      entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
      entity.Property(e => e.MinStockQuantity).HasDefaultValue(0);
      entity.HasOne(e => e.Company)
        .WithMany()
        .HasForeignKey(e => e.CompanyId)
        .OnDelete(DeleteBehavior.Cascade);
      entity.HasOne(e => e.Category)
        .WithMany()
        .HasForeignKey(e => e.CategoryId)
        .OnDelete(DeleteBehavior.SetNull);
    });

    modelBuilder.Entity<Supplier>(entity =>
    {
      entity.ToTable("Suppliers");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
      entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
      entity.Property(e => e.CNPJ).IsRequired().HasMaxLength(20);
      entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
      entity.Property(e => e.Address).IsRequired().HasMaxLength(200);
      entity.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<Finance>(entity =>
    {
      entity.ToTable("Finances");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Type).IsRequired().HasMaxLength(20);
      entity.Property(e => e.Description).IsRequired().HasMaxLength(200);
      entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
      entity.Property(e => e.DueDate).IsRequired();
      entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
      entity.Property(e => e.CreatedAt).IsRequired();
      entity.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<Warehouse>(entity =>
    {
      entity.ToTable("Warehouses");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
      entity.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<StockMove>(entity =>
    {
      entity.ToTable("StockMoves");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Quantity).IsRequired();
      entity.Property(e => e.Date).IsRequired();
      entity.Property(e => e.Type).IsRequired();
      entity.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
      entity.HasOne(e => e.Warehouse)
                .WithMany()
                .HasForeignKey(e => e.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
      entity.HasOne(e => e.Supplier)
                .WithMany()
                .HasForeignKey(e => e.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);
      entity.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<StockBalance>(entity =>
    {
      entity.ToTable("StockBalances");
      entity.HasKey(e => new { e.ProductId, e.WarehouseId, e.CompanyId });
      entity.Property(e => e.Quantity).IsRequired();
      entity.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
      entity.HasOne(e => e.Warehouse)
                .WithMany()
                .HasForeignKey(e => e.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);
      entity.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<Sale>(entity =>
    {
      entity.ToTable("Sales");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Date).IsRequired();
      entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
      entity.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
      entity.HasMany(e => e.Items)
                .WithOne(e => e.Sale)
                .HasForeignKey(e => e.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
    });

    modelBuilder.Entity<SaleItem>(entity =>
    {
      entity.ToTable("SaleItems");
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Quantity).IsRequired();
      entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
      entity.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
      entity.HasOne(e => e.Sale)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
      entity.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
    });
  }
}