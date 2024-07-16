﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WalkAPI.Data;

#nullable disable

namespace WalkAPI.Migrations
{
    [DbContext(typeof(NZWalkDbContext))]
    partial class NZWalkDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WalkAPI.Models.Domain.Difficulty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8626d6a1-9e34-4250-a94f-6a7203af9b80"),
                            Name = "Easy"
                        },
                        new
                        {
                            Id = new Guid("6732d6ee-977c-4081-aff1-f3e7c9a4f6e8"),
                            Name = "Medium"
                        },
                        new
                        {
                            Id = new Guid("51324663-b646-487b-b7b5-185a7e68fee7"),
                            Name = "Hard"
                        });
                });

            modelBuilder.Entity("WalkAPI.Models.Domain.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegionImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Regions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("be2e701f-2fe5-4e75-927e-8f1bbaee274b"),
                            Code = "BNH",
                            Name = "Hieu",
                            RegionImgUrl = "https://www.bing.com/images/search?view=detailV2&ccid=2TumufLJ&id=BB5718BF86899F23683761741B9FFD007A8EE8F6&thid=OIP.2TumufLJYnKvtmU6UMWe6wHaE8&mediaurl=https%3a%2f%2fwww.imgacademy.com%2fsites%2fdefault%2ffiles%2flegacy-hotel-20.jpg&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.d93ba6b9f2c96272afb6653a50c59eeb%3frik%3d9uiOegD9nxt0YQ%26pid%3dImgRaw%26r%3d0&exph=1067&expw=1600&q=img&simid=607990275582734595&FORM=IRPRST&ck=F13EACAFAEF9792505C5B101D9D2052D&selectedIndex=0&itb=0"
                        },
                        new
                        {
                            Id = new Guid("1a95b499-1c1c-4f43-8d5f-f1e7dfd9bcc9"),
                            Code = "KV",
                            Name = "Khanh"
                        },
                        new
                        {
                            Id = new Guid("3dcd48d5-4ceb-47de-a99f-1b5284ce0b10"),
                            Code = "VHH",
                            Name = "Huy",
                            RegionImgUrl = "https://www.bing.com/images/search?view=detailV2&ccid=2TumufLJ&id=BB5718BF86899F23683761741B9FFD007A8EE8F6&thid=OIP.2TumufLJYnKvtmU6UMWe6wHaE8&mediaurl=https%3a%2f%2fwww.imgacademy.com%2fsites%2fdefault%2ffiles%2flegacy-hotel-20.jpg&cdnurl=https%3a%2f%2fth.bing.com%2fth%2fid%2fR.d93ba6b9f2c96272afb6653a50c59eeb%3frik%3d9uiOegD9nxt0YQ%26pid%3dImgRaw%26r%3d0&exph=1067&expw=1600&q=img&simid=607990275582734595&FORM=IRPRST&ck=F13EACAFAEF9792505C5B101D9D2052D&selectedIndex=0&itb=0"
                        });
                });

            modelBuilder.Entity("WalkAPI.Models.Domain.Walk", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DifficultyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("LengthInKm")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("WalkImgUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("RegionId");

                    b.ToTable("Walks");
                });

            modelBuilder.Entity("WalkAPI.Models.Domain.Walk", b =>
                {
                    b.HasOne("WalkAPI.Models.Domain.Difficulty", "Difficulty")
                        .WithMany()
                        .HasForeignKey("DifficultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WalkAPI.Models.Domain.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Difficulty");

                    b.Navigation("Region");
                });
#pragma warning restore 612, 618
        }
    }
}
