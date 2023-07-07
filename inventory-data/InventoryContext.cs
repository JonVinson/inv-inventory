using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace inventory_data;

public partial class InventoryContext : DbContext
{
    public InventoryContext()
    {
    }

    public InventoryContext(DbContextOptions<InventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<InventoryItem> InventoryItems { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ELLA;Initial Catalog=Inventory;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InventoryItem>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_InventoryItems_ProductId");

            entity.HasOne(d => d.Product).WithMany(p => p.InventoryItems).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.ManufacturerId, "IX_Products_ManufacturerId");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products).HasForeignKey(d => d.ManufacturerId);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasIndex(e => e.CustomerId, "IX_Transactions_CustomerId");

            entity.HasIndex(e => e.EmployeeId, "IX_Transactions_EmployeeId");

            entity.HasIndex(e => e.ProductId, "IX_Transactions_ProductId");

            entity.HasIndex(e => e.SupplierId, "IX_Transactions_SupplierId");

            entity.Property(e => e.Date).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Transactions).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.Employee).WithMany(p => p.Transactions).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.Product).WithMany(p => p.Transactions).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Supplier).WithMany(p => p.Transactions).HasForeignKey(d => d.SupplierId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
