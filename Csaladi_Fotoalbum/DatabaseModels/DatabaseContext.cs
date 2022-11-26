using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Csaladi_Fotoalbum.DatabaseModels;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Connection> Connections { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\walte\\source\\repos\\Csaladi_Fotoalbum\\Csaladi_Fotoalbum\\DATABASE.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__albums__3213E83F4F3C89D7");

            entity.ToTable("albums");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Color).HasColumnName("color");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__colors__3213E83F61424073");

            entity.ToTable("colors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Colorcode)
                .HasColumnType("text")
                .HasColumnName("colorcode");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3213E83F46AE9D64");

            entity.ToTable("connections");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Album).HasColumnName("album");
            entity.Property(e => e.Person).HasColumnName("person");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__location__3213E83FCCC0D90B");

            entity.ToTable("locations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__people__3213E83FA180FDA4");

            entity.ToTable("people");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("text")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
