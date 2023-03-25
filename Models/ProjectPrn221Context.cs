﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Project_PRN.Models;

public partial class ProjectPrn221Context : DbContext
{
    public ProjectPrn221Context()
    {
    }

    public ProjectPrn221Context(DbContextOptions<ProjectPrn221Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetLocation> AssetLocations { get; set; }

    public virtual DbSet<AssetStatus> AssetStatuses { get; set; }

    public virtual DbSet<AssetTransaction> AssetTransactions { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    public virtual DbSet<AssetVendor> AssetVendors { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(local);Database=Project_PRN221;uid=sa;pwd=12345678;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Assets__3213E83F2DF82C78");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcquisitionDate)
                .HasColumnType("date")
                .HasColumnName("acquisition_date");
            entity.Property(e => e.AssignDate)
                .HasColumnType("date")
                .HasColumnName("assign_date");
            entity.Property(e => e.AssigneeId).HasColumnName("assignee_id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.CreateByUser).HasColumnName("create_by_user");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DisposalDate)
                .HasColumnType("date")
                .HasColumnName("disposal_date");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Assignee).WithMany(p => p.AssetAssignees)
                .HasForeignKey(d => d.AssigneeId)
                .HasConstraintName("FK__Assets__assignee__32E0915F");

            entity.HasOne(d => d.CreateByUserNavigation).WithMany(p => p.AssetCreateByUserNavigations)
                .HasForeignKey(d => d.CreateByUser)
                .HasConstraintName("FK__Assets__create_b__35BCFE0A");

            entity.HasOne(d => d.Location).WithMany(p => p.Assets)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("FK__Assets__location__30F848ED");

            entity.HasOne(d => d.Status).WithMany(p => p.Assets)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Assets__status_i__31EC6D26");

            entity.HasOne(d => d.Transaction).WithMany(p => p.Assets)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__Assets__transact__34C8D9D1");

            entity.HasOne(d => d.Type).WithMany(p => p.Assets)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Assets__type_id__300424B4");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Assets)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK__Assets__vendor_i__33D4B598");
        });

        modelBuilder.Entity<AssetLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AssetLoc__3213E83F0126BCB0");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AssetStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AssetSta__3213E83FF2865E27");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AvailableToUse).HasColumnName("available_to_use");
            entity.Property(e => e.CurrentlyInUse).HasColumnName("currently_in_use");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AssetTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AssetTra__3213E83FD9DB5727");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.TransactionCost).HasColumnName("transaction_cost");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("date")
                .HasColumnName("transaction_date");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("transaction_type");
        });

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AssetTyp__3213E83F79123B7B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<AssetVendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AssetVen__3213E83F2C8AD0E2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.ContactName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contact_name");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(63)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("website");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F00A2AE00");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(63)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.UserCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("user_code");
            entity.Property(e => e.UserIdNumber)
                .HasMaxLength(63)
                .IsUnicode(false)
                .HasColumnName("user_id_number");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
