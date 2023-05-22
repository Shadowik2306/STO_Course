﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using STODatabaseImplement;

#nullable disable

namespace STODatabaseImplement.Migrations
{
    [DbContext(typeof(STODatabase))]
    [Migration("20230521201323_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("STODatabaseImplement.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.CarSpare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("SpareId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("SpareId");

                    b.ToTable("CarSpares");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Employer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employers");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Maintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Cost")
                        .HasColumnType("float");

                    b.Property<DateTime>("DateCreate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployerId");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.MaintenanceCar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CarId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("MaintenanceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("MaintenanceId");

                    b.ToTable("MaintenanceCars");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CarId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Spare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Spares");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Storekeeper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Storekeepers");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Work", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DurationId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("StorekeeperId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DurationId");

                    b.HasIndex("StorekeeperId");

                    b.ToTable("Works");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkDuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("WorkDurations");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkMaintenance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("MaintenanceId")
                        .HasColumnType("int");

                    b.Property<int>("MaintenanceId")
                        .HasColumnType("int");

                    b.Property<int>("WorkId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaintenanceId");

                    b.HasIndex("WorkId");

                    b.ToTable("WorkMaintenances");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkSpare", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("SpareId")
                        .HasColumnType("int");

                    b.Property<int>("WorkId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpareId");

                    b.HasIndex("WorkId");

                    b.ToTable("WorkSpares");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.CarSpare", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.Car", "Car")
                        .WithMany("Spares")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STODatabaseImplement.Models.Spare", "Spare")
                        .WithMany()
                        .HasForeignKey("SpareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Spare");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Maintenance", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.Employer", null)
                        .WithMany("Maintenances")
                        .HasForeignKey("EmployerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("STODatabaseImplement.Models.MaintenanceCar", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STODatabaseImplement.Models.Maintenance", "Maintenance")
                        .WithMany("Cars")
                        .HasForeignKey("MaintenanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Maintenance");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Service", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.Car", null)
                        .WithMany("Services")
                        .HasForeignKey("CarId");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Work", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.WorkDuration", null)
                        .WithMany("Works")
                        .HasForeignKey("DurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STODatabaseImplement.Models.Storekeeper", null)
                        .WithMany("Works")
                        .HasForeignKey("StorekeeperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkMaintenance", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.Maintenance", "Maintenance")
                        .WithMany()
                        .HasForeignKey("MaintenanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STODatabaseImplement.Models.Work", "Work")
                        .WithMany("Maintenances")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maintenance");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkSpare", b =>
                {
                    b.HasOne("STODatabaseImplement.Models.Spare", "Spare")
                        .WithMany()
                        .HasForeignKey("SpareId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("STODatabaseImplement.Models.Work", "Work")
                        .WithMany("Spares")
                        .HasForeignKey("WorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spare");

                    b.Navigation("Work");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Car", b =>
                {
                    b.Navigation("Services");

                    b.Navigation("Spares");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Employer", b =>
                {
                    b.Navigation("Maintenances");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Maintenance", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Storekeeper", b =>
                {
                    b.Navigation("Works");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.Work", b =>
                {
                    b.Navigation("Maintenances");

                    b.Navigation("Spares");
                });

            modelBuilder.Entity("STODatabaseImplement.Models.WorkDuration", b =>
                {
                    b.Navigation("Works");
                });
#pragma warning restore 612, 618
        }
    }
}
