﻿// <auto-generated />
using System;
using EntitiyFrameworkRelations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EntitiyFrameworkRelations.Migrations
{
    [DbContext(typeof(BrickContext))]
    partial class BrickContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BrickTag", b =>
                {
                    b.Property<int>("BricksId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("BricksId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BrickTag");
                });

            modelBuilder.Entity("EntitiyFrameworkRelations.Models.Brick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Color")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Bricks");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Brick");
                });

            modelBuilder.Entity("EntitiyFrameworkRelations.Models.BrickAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableAmount")
                        .HasColumnType("int");

                    b.Property<int?>("BrickId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int?>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrickId");

                    b.HasIndex("VendorId");

                    b.ToTable("Availabilities");
                });

            modelBuilder.Entity("EntitiyFrameworkRelations.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("EntitiyFrameworkRelations.Models.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("VendorName")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("EntitiyFrameworkRelations.Models.BasePlate", b =>
                {
                    b.HasBaseType("EntitiyFrameworkRelations.Models.Brick");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("BasePlate");
                });

            modelBuilder.Entity("EntitiyFrameworkRelations.Models.MinifigHead", b =>
                {
                    b.HasBaseType("EntitiyFrameworkRelations.Models.Brick");

                    b.Property<bool>("IsDualSided")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("MinifigHead");
                });

            modelBuilder.Entity("BrickTag", b =>
                {
                    b.HasOne("EntitiyFrameworkRelations.Models.Brick", null)
                        .WithMany()
                        .HasForeignKey("BricksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntitiyFrameworkRelations.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EntitiyFrameworkRelations.Models.BrickAvailability", b =>
                {
                    b.HasOne("EntitiyFrameworkRelations.Models.Brick", "Brick")
                        .WithMany("Availabilities")
                        .HasForeignKey("BrickId");

                    b.HasOne("EntitiyFrameworkRelations.Models.Vendor", "Vendor")
                        .WithMany("Availabilities")
                        .HasForeignKey("VendorId");

                    b.Navigation("Brick");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EntitiyFrameworkRelations.Models.Brick", b =>
                {
                    b.Navigation("Availabilities");
                });

            modelBuilder.Entity("EntitiyFrameworkRelations.Models.Vendor", b =>
                {
                    b.Navigation("Availabilities");
                });
#pragma warning restore 612, 618
        }
    }
}