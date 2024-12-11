using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Crud.Server.Models;

public partial class DbcrudSeminarioContext : DbContext
{
    public DbcrudSeminarioContext()
    {
    }

    public DbcrudSeminarioContext(DbContextOptions<DbcrudSeminarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Producto__3214EC06D8160F55")
                .IsClustered(false);

            entity.ToTable("Producto");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();  

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();

            entity.Property(e => e.Precio).HasColumnType("FLOAT");  
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
