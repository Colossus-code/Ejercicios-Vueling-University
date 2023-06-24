﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApiNet6.Models;

public partial class TaskAssignerDbContext : DbContext
{
    public TaskAssignerDbContext()
    {
    }

    public TaskAssignerDbContext(DbContextOptions<TaskAssignerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Itworkers> Itworkers { get; set; }

    public virtual DbSet<Tasks> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-H4OQRVK;Initial Catalog=TaskAssignerDb;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Itworkers>(entity =>
        {
            entity.HasKey(e => e.IdWorker).HasName("PK_ITWorker");

            entity.ToTable("ITWorkers");

            entity.HasIndex(e => e.IdTask, "IX_TaskId").IsUnique();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Tasks>(entity =>
        {
            entity.HasKey(e => e.IdTask);

            entity.HasIndex(e => e.IdWorker, "IX_ItWorker").IsUnique();

            entity.Property(e => e.TaskDescription)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.TaskName)
                .IsRequired()
                .HasMaxLength(15);

            entity.HasOne(d => d.IdWorkerNavigation).WithOne(p => p.Tasks)
                .HasForeignKey<Tasks>(d => d.IdWorker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_ITWorkers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}