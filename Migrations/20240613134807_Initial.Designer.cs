﻿// <auto-generated />
using System;
using System.Collections.Generic;
using JSON_Market.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JSON_Market.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240613134807_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JSON_Market.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("JSON_Market.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("JSON_Market.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<byte>("Discount")
                        .HasColumnType("smallint");

                    b.Property<List<string>>("ImageUrls")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("SellerId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("JSON_Market.Models.Seller", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Seller");
                });

            modelBuilder.Entity("JSON_Market.Models.Order", b =>
                {
                    b.HasOne("JSON_Market.Models.Customer", "Customer")
                        .WithMany("OrderHistory")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("JSON_Market.Models.Product", b =>
                {
                    b.HasOne("JSON_Market.Models.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");

                    b.HasOne("JSON_Market.Models.Seller", "Seller")
                        .WithMany("CreatedProducts")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("JSON_Market.Models.Customer", b =>
                {
                    b.Navigation("OrderHistory");
                });

            modelBuilder.Entity("JSON_Market.Models.Order", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("JSON_Market.Models.Seller", b =>
                {
                    b.Navigation("CreatedProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
