using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WMSLite.Api.Models;

namespace WMSLite.Api.Data;

public partial class WMSLiteDbContext : DbContext
{
    public WMSLiteDbContext()
    {
    }

    public WMSLiteDbContext(DbContextOptions<WMSLiteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contractor> Contractors { get; set; }

    public virtual DbSet<DocumentItem> DocumentItems { get; set; }

    public virtual DbSet<GoodsReceiptDocument> GoodsReceiptDocuments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-9OP7BRT;Database=WMSLiteDB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contractor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contract__3214EC076EFC6531");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Symbol).HasMaxLength(50);
        });

        modelBuilder.Entity<DocumentItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC07950E26FF");

            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.UnitOfMeasure).HasMaxLength(50);
        });

        modelBuilder.Entity<GoodsReceiptDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GoodsRec__3214EC07E5CAAD06");

            entity.Property(e => e.Symbol).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
