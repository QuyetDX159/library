using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace library.Models.Entities;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categorys { get; set; }

    public virtual DbSet<Content> Contents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }
/*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AdminDXQ;Initial Catalog=Library;User ID=sa;Password=ailaai21;Integrated Security=True;TrustServerCertificate=True");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.IdB);

            entity.Property(e => e.IdB).HasColumnName("id_b");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .HasColumnName("author");
            entity.Property(e => e.BookName)
                .HasMaxLength(50)
                .HasColumnName("book_name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdCa).HasColumnName("id_ca");
            entity.Property(e => e.PublicationYear)
                .HasColumnType("date")
                .HasColumnName("publication_year");
            entity.Property(e => e.Thumbnail)
                .HasMaxLength(500)
                .HasColumnName("thumbnail");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.IdCaNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.IdCa)
                .HasConstraintName("FK_Books_Categorys");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCa).HasName("PK_category");

            entity.Property(e => e.IdCa).HasColumnName("id_ca");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Content>(entity =>
        {
            entity.HasKey(e => e.IdC).HasName("PK_content");

            entity.Property(e => e.IdC).HasColumnName("id_c");
            entity.Property(e => e.IdB).HasColumnName("id_b");
            entity.Property(e => e.Img1)
                .HasMaxLength(500)
                .HasColumnName("img_1");
            entity.Property(e => e.Img2)
                .HasMaxLength(500)
                .HasColumnName("img_2");
            entity.Property(e => e.Paragraph1).HasColumnName("paragraph_1");
            entity.Property(e => e.Paragraph2).HasColumnName("paragraph_2");
            entity.Property(e => e.Paragraph3).HasColumnName("paragraph_3");

            entity.HasOne(d => d.IdBNavigation).WithMany(p => p.Contents)
                .HasForeignKey(d => d.IdB)
                .HasConstraintName("FK_Contents_Books");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole);

            entity.Property(e => e.IdRole).HasColumnName("id_role");
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdU);

            entity.Property(e => e.IdU).HasColumnName("id_u");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .HasColumnName("fullname");
            entity.Property(e => e.IdRole).HasColumnName("id_role"); 
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.ConfirmPassword)
                .HasMaxLength(50)
                .HasColumnName("confirmPassword");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .HasColumnName("phone");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
