using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyProject.Models;

public partial class TaskMsContext : DbContext
{
    public TaskMsContext()
    {
    }

    public TaskMsContext(DbContextOptions<TaskMsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Register> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ZAINAB;Database=Task_MS;Trusted_Connection=True;trustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Task__3214EC075B9E26FE");

            entity.ToTable("Task");

            entity.Property(e => e.EndDate).HasColumnName("End_Date");
            entity.Property(e => e.StartDate).HasColumnName("Start_Date");
            entity.Property(e => e.TaskDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("Task_Description");
            entity.Property(e => e.TaskName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Task_Name");
            entity.Property(e => e.TaskType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Task_Type");
        });

        modelBuilder.Entity<Register>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07EED8242B");

            entity.ToTable("Users");

            entity.Property(e => e.ConfirmPassword)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
