﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NudeAssignment.Models;

namespace NudeAssignment.Migrations
{
    [DbContext(typeof(_CoverageDBContext))]
    [Migration("20190926223219_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NudeAssignment.Models.CoverageItem", b =>
                {
                    b.Property<Guid>("ItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Name");

                    b.Property<int>("Value");

                    b.HasKey("ItemId");

                    b.ToTable("CoverageItems");
                });

            modelBuilder.Entity("NudeAssignment.Models.Customer", b =>
                {
                    b.Property<long>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NudeAssignment.Models.CustomerItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CustomerId");

                    b.Property<Guid>("ItemId");

                    b.HasKey("Id");

                    b.ToTable("CustomerItems");
                });

            modelBuilder.Entity("NudeAssignment.Models.LKPCoverageItemCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("LKPCoverageItemCategories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Description = "Cloth items, jacket etc.",
                            Name = "Clothing"
                        },
                        new
                        {
                            CategoryId = 2,
                            Description = "TV, Radio, Playstation...",
                            Name = "Electroics"
                        },
                        new
                        {
                            CategoryId = 3,
                            Description = "Pots, Flatware...",
                            Name = "kitchen"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
