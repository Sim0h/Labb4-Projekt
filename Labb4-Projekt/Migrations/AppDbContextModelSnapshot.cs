﻿// <auto-generated />
using System;
using Labb4_Projekt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Labb4_Projekt.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClassLibraryLabb4.Appointment", b =>
                {
                    b.Property<int>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentID"));

                    b.Property<DateTime?>("AppointmentEnd")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<double>("AppointmentLength")
                        .HasColumnType("float");

                    b.Property<string>("AppointmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("AppointmentStart")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int>("CustomerID")
                        .HasColumnType("int");

                    b.HasKey("AppointmentID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            AppointmentID = 1,
                            AppointmentEnd = new DateTime(2024, 5, 8, 11, 30, 0, 0, DateTimeKind.Unspecified),
                            AppointmentLength = 1.0,
                            AppointmentName = "Standard Läkarbesök",
                            AppointmentStart = new DateTime(2024, 5, 8, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            CompanyID = 1,
                            CustomerID = 1
                        },
                        new
                        {
                            AppointmentID = 2,
                            AppointmentEnd = new DateTime(2024, 5, 7, 16, 30, 0, 0, DateTimeKind.Unspecified),
                            AppointmentLength = 3.0,
                            AppointmentName = "Ögonkontroll",
                            AppointmentStart = new DateTime(2024, 5, 7, 13, 30, 0, 0, DateTimeKind.Unspecified),
                            CompanyID = 1,
                            CustomerID = 2
                        },
                        new
                        {
                            AppointmentID = 3,
                            AppointmentEnd = new DateTime(2024, 5, 10, 8, 30, 0, 0, DateTimeKind.Unspecified),
                            AppointmentLength = 0.5,
                            AppointmentName = "Tandläkarbesök",
                            AppointmentStart = new DateTime(2024, 5, 10, 8, 0, 0, 0, DateTimeKind.Unspecified),
                            CompanyID = 1,
                            CustomerID = 2
                        });
                });

            modelBuilder.Entity("ClassLibraryLabb4.ChangeHistory", b =>
                {
                    b.Property<int>("ChangeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChangeID"));

                    b.Property<string>("ChangeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NewAppointmentTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("OldAppointmentTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("WhenChanged")
                        .HasColumnType("datetime2");

                    b.HasKey("ChangeID");

                    b.ToTable("ChangeHistorys");

                    b.HasData(
                        new
                        {
                            ChangeID = 1,
                            ChangeType = "Ombokning",
                            NewAppointmentTime = new DateTime(2024, 5, 15, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            OldAppointmentTime = new DateTime(2024, 5, 14, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            WhenChanged = new DateTime(2024, 5, 14, 10, 30, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ClassLibraryLabb4.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyID"));

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyID");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            CompanyID = 1,
                            CompanyName = "SUT23 Kliniken"
                        });
                });

            modelBuilder.Entity("ClassLibraryLabb4.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerID"));

                    b.Property<string>("CustomerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerID = 1,
                            CustomerAddress = "SolGatan 12B",
                            CustomerEmail = "Sam@testing.se",
                            CustomerName = "Sam Testing",
                            CustomerPhone = "0712365987"
                        },
                        new
                        {
                            CustomerID = 2,
                            CustomerAddress = "Varbergsgatan 31",
                            CustomerEmail = "Simon@ståhl.se",
                            CustomerName = "Simon Ståhl",
                            CustomerPhone = "0744556698"
                        },
                        new
                        {
                            CustomerID = 3,
                            CustomerAddress = "Storgatan 6",
                            CustomerEmail = "Henrik@johansson.se",
                            CustomerName = "Henrik Johansson",
                            CustomerPhone = "0723647895"
                        });
                });

            modelBuilder.Entity("ClassLibraryLabb4.Appointment", b =>
                {
                    b.HasOne("ClassLibraryLabb4.Company", "Company")
                        .WithMany("Appointments")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassLibraryLabb4.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ClassLibraryLabb4.Company", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("ClassLibraryLabb4.Customer", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}