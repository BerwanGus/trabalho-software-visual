﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InventarioRoupasAPI.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("API.Models.ProductSale", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SaleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleId");

                    b.ToTable("ProductsSales");
                });

            modelBuilder.Entity("APISale.Models.Client", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Purchases_Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Register_Date")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("APISale.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Event_Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Sales_Quantity")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("APISale.Models.Sale", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Client_Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Event_Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Sale_Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Seller_Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("Client_Id");

                    b.HasIndex("Event_Id");

                    b.HasIndex("Seller_Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("APISale.Models.Seller", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Sales_Quantity")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Sellers");
                });

            modelBuilder.Entity("APIStock.Models.Brand", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("APIStock.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand_Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Condition")
                        .HasColumnType("TEXT");

                    b.Property<float>("Cost")
                        .HasColumnType("REAL");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<float>("Price")
                        .HasColumnType("REAL");

                    b.Property<string>("Size")
                        .HasColumnType("TEXT");

                    b.Property<string>("Style")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type_Id")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Brand_Id");

                    b.HasIndex("Type_Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("APIStock.Models.ProductType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Style")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("API.Models.ProductSale", b =>
                {
                    b.HasOne("APIStock.Models.Product", "Product")
                        .WithMany("ProductSales")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APISale.Models.Sale", "Sale")
                        .WithMany("ProductSales")
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("APISale.Models.Sale", b =>
                {
                    b.HasOne("APISale.Models.Client", "Client")
                        .WithMany("Purchases")
                        .HasForeignKey("Client_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APISale.Models.Event", "Event")
                        .WithMany("Sales")
                        .HasForeignKey("Event_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APISale.Models.Seller", "Seller")
                        .WithMany("Sales")
                        .HasForeignKey("Seller_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Event");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("APIStock.Models.Product", b =>
                {
                    b.HasOne("APIStock.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("Brand_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APIStock.Models.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("Type_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("APISale.Models.Client", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("APISale.Models.Event", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("APISale.Models.Sale", b =>
                {
                    b.Navigation("ProductSales");
                });

            modelBuilder.Entity("APISale.Models.Seller", b =>
                {
                    b.Navigation("Sales");
                });

            modelBuilder.Entity("APIStock.Models.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("APIStock.Models.Product", b =>
                {
                    b.Navigation("ProductSales");
                });

            modelBuilder.Entity("APIStock.Models.ProductType", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
