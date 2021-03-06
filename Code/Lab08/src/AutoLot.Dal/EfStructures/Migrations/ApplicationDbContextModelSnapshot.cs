﻿// Copyright Information
// ==================================
// AutoLot - AutoLot.Dal - ApplicationDbContextModelSnapshot.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 2020/08/08
// See License.txt for more information
// ==================================

using System;
using AutoLot.Dal.EfStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AutoLot.Dal.EfStructures.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AutoLot.Models.Entities.Car", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Color")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);

                b.Property<int>("MakeId")
                    .HasColumnType("int");

                b.Property<string>("PetName")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);

                b.Property<byte[]>("TimeStamp")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Id");

                b.HasIndex("MakeId");

                b.ToTable("Inventory", "Dbo");
            });

            modelBuilder.Entity("AutoLot.Models.Entities.CreditRisk", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("CustomerId")
                    .HasColumnType("int");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnName("FirstName")
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnName("LastName")
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);

                b.Property<byte[]>("TimeStamp")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Id");

                b.HasIndex("CustomerId");

                b.HasIndex("FirstName", "LastName")
                    .IsUnique();

                b.ToTable("CreditRisks", "Dbo");
            });

            modelBuilder.Entity("AutoLot.Models.Entities.Customer", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<byte[]>("TimeStamp")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Id");

                b.ToTable("Customers", "Dbo");
            });

            modelBuilder.Entity("AutoLot.Models.Entities.Make", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(50)")
                    .HasMaxLength(50);

                b.Property<byte[]>("TimeStamp")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Id");

                b.ToTable("Makes", "dbo");
            });

            modelBuilder.Entity("AutoLot.Models.Entities.Order", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int")
                    .HasAnnotation("SqlServer:ValueGenerationStrategy",
                        SqlServerValueGenerationStrategy.IdentityColumn);

                b.Property<int>("CarId")
                    .HasColumnType("int");

                b.Property<int>("CustomerId")
                    .HasColumnType("int");

                b.Property<byte[]>("TimeStamp")
                    .IsConcurrencyToken()
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("rowversion");

                b.HasKey("Id");

                b.HasIndex("CarId");

                b.HasIndex("CustomerId", "CarId")
                    .IsUnique();

                b.ToTable("Orders", "Dbo");
            });

            modelBuilder.Entity("AutoLot.Models.Entities.Car", b =>
            {
                b.HasOne("AutoLot.Models.Entities.Make", "MakeNavigation")
                    .WithMany("Cars")
                    .HasForeignKey("MakeId")
                    .HasConstraintName("FK_Make_Inventory")
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            modelBuilder.Entity("AutoLot.Models.Entities.CreditRisk", b =>
            {
                b.HasOne("AutoLot.Models.Entities.Customer", "CustomerNavigation")
                    .WithMany("CreditRisks")
                    .HasForeignKey("CustomerId")
                    .HasConstraintName("FK_CreditRisks_Customers")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.OwnsOne("AutoLot.Models.Entities.Owned.Person", "PersonalInformation", b1 =>
                {
                    b1.Property<int>("CreditRiskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b1.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b1.HasKey("CreditRiskId");

                    b1.ToTable("CreditRisks");

                    b1.WithOwner()
                        .HasForeignKey("CreditRiskId");
                });
            });

            modelBuilder.Entity("AutoLot.Models.Entities.Customer", b =>
            {
                b.OwnsOne("AutoLot.Models.Entities.Owned.Person", "PersonalInformation", b1 =>
                {
                    b1.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn);

                    b1.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("FirstName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b1.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnName("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b1.HasKey("CustomerId");

                    b1.ToTable("Customers");

                    b1.WithOwner()
                        .HasForeignKey("CustomerId");
                });
            });

            modelBuilder.Entity("AutoLot.Models.Entities.Order", b =>
            {
                b.HasOne("AutoLot.Models.Entities.Car", "CarNavigation")
                    .WithMany("Orders")
                    .HasForeignKey("CarId")
                    .HasConstraintName("FK_Orders_Inventory")
                    .IsRequired();

                b.HasOne("AutoLot.Models.Entities.Customer", "CustomerNavigation")
                    .WithMany("Orders")
                    .HasForeignKey("CustomerId")
                    .HasConstraintName("FK_Orders_Customers")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}