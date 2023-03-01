﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Taxes.Data;

#nullable disable

namespace Taxes.Migrations
{
    [DbContext(typeof(DBDataContext))]
    partial class DBDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Taxes.Entities.Municipality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Municipality");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"),
                            Name = "Copenhagen"
                        });
                });

            modelBuilder.Entity("Taxes.Entities.Tax", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MunicipalityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TaxTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MunicipalityId");

                    b.HasIndex("TaxTypeId");

                    b.ToTable("Tax");

                    b.HasData(
                        new
                        {
                            Id = new Guid("091cd1c2-26d4-4af1-9152-df5a59e7c648"),
                            MunicipalityId = new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"),
                            TaxTypeId = new Guid("19064510-fdad-41ec-a52b-f9aad3099549"),
                            ValidFrom = new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Value = 0.20000000000000001
                        },
                        new
                        {
                            Id = new Guid("4b114786-b308-46af-b371-5634f062871b"),
                            MunicipalityId = new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"),
                            TaxTypeId = new Guid("a7d328d1-0c15-4fd5-a5a2-f4dbe5ddb3e8"),
                            ValidFrom = new DateTime(2016, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Value = 0.40000000000000002
                        },
                        new
                        {
                            Id = new Guid("0bef2975-c8d4-4e28-ae0f-7f030663969c"),
                            MunicipalityId = new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"),
                            TaxTypeId = new Guid("9720e647-f87c-4fed-a84d-c18e4f7a6024"),
                            ValidFrom = new DateTime(2016, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Value = 0.10000000000000001
                        },
                        new
                        {
                            Id = new Guid("42e189cb-7963-4fd5-a3f7-4cf8db7d1b3b"),
                            MunicipalityId = new Guid("a8577c30-bd98-4bee-b1ed-bede0812df24"),
                            TaxTypeId = new Guid("9720e647-f87c-4fed-a84d-c18e4f7a6024"),
                            ValidFrom = new DateTime(2016, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Value = 0.10000000000000001
                        });
                });

            modelBuilder.Entity("Taxes.Entities.TaxType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("Priority")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("TaxType");

                    b.HasData(
                        new
                        {
                            Id = new Guid("19064510-fdad-41ec-a52b-f9aad3099549"),
                            Name = "yearly",
                            Priority = (short)1
                        },
                        new
                        {
                            Id = new Guid("a7d328d1-0c15-4fd5-a5a2-f4dbe5ddb3e8"),
                            Name = "montly",
                            Priority = (short)2
                        },
                        new
                        {
                            Id = new Guid("c427ee85-afe4-4910-90c2-e42d19149821"),
                            Name = "weekly",
                            Priority = (short)3
                        },
                        new
                        {
                            Id = new Guid("9720e647-f87c-4fed-a84d-c18e4f7a6024"),
                            Name = "daily",
                            Priority = (short)4
                        });
                });

            modelBuilder.Entity("Taxes.Entities.Tax", b =>
                {
                    b.HasOne("Taxes.Entities.Municipality", null)
                        .WithMany("Tax")
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Taxes.Entities.TaxType", null)
                        .WithMany("Tax")
                        .HasForeignKey("TaxTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Taxes.Entities.Municipality", b =>
                {
                    b.Navigation("Tax");
                });

            modelBuilder.Entity("Taxes.Entities.TaxType", b =>
                {
                    b.Navigation("Tax");
                });
#pragma warning restore 612, 618
        }
    }
}
