﻿using AutoLot.Models.Entities;
using AutoLot.Models.Entities.Owned;
using AutoLot.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AutoLot.Dal.EfStructures
{
    public sealed class ApplicationDbContext : DbContext
    {
        public int MakeId { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CreditRisk> CreditRisks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomerOrderViewModel> CustomerOrderViewModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity => {
                entity.HasQueryFilter(c => c.MakeId == MakeId);
            });

            modelBuilder.Entity<CreditRisk>(entity =>
            {
                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.CreditRisks)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_CreditRisks_Customers");

                entity
                    .Property<string>(nameof(Person.FirstName))
                    .HasColumnName(nameof(Person.FirstName))
                    .HasMaxLength(50)
                    .IsRequired(true);

                entity
                    .Property<string>(nameof(Person.LastName))
                    .HasColumnName(nameof(Person.LastName))
                    .HasMaxLength(50)
                    .IsRequired(true);

                entity.OwnsOne(o => o.PersonalInformation,
                    pd =>
                    {
                        pd.Property<string>(nameof(Person.FirstName))
                            .HasColumnName(nameof(Person.FirstName))
                            .HasColumnType("nvarchar(50)");
                        pd.Property<string>(nameof(Person.LastName))
                            .HasColumnName(nameof(Person.LastName))
                            .HasColumnType("nvarchar(50)");
                    });
                entity.HasIndex(nameof(Person.FirstName), nameof(Person.LastName)).IsUnique(true);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.OwnsOne(o => o.PersonalInformation,
                    pd =>
                    {
                        pd.Property(p => p.FirstName).HasColumnName(nameof(Person.FirstName));
                        pd.Property(p => p.LastName).HasColumnName(nameof(Person.LastName));
                    });
            });

            modelBuilder.Entity<Make>(entity =>
              {
                  entity.HasMany(e => e.Cars)
                      .WithOne(c => c.MakeNavigation)
                      .HasForeignKey(k => k.MakeId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Make_Inventory");
              });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(d => d.CarNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Inventory");

                entity.HasOne(d => d.CustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Orders_Customers");
                entity.HasIndex(cr => new { cr.CustomerId, cr.CarId }).IsUnique(true);
            });

            modelBuilder.Entity<CustomerOrderViewModel>(entity =>
            {
                entity.HasNoKey().ToView("CustomerOrderView", "dbo");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
