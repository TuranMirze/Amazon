using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Models;

public partial class AmazonContext : DbContext
{
    public AmazonContext()
    {
    }

    public AmazonContext(DbContextOptions<AmazonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Basket> Baskets { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MJLOCD2\\SQLEXPRESS;Database=Amazon;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Basket>(entity =>
        {
            entity.ToTable("baskets");

            entity.HasIndex(e => e.ProductId, "IX_baskets_ProductId");

            entity.HasIndex(e => e.UsersId, "IX_baskets_UsersId");

            entity.HasOne(d => d.Product).WithMany(p => p.Baskets).HasForeignKey(d => d.ProductId);

            entity.HasOne(d => d.Users).WithMany(p => p.Baskets).HasForeignKey(d => d.UsersId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
