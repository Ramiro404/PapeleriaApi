﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PapeleriaApi.Modelos.Datos;

#nullable disable

namespace PapeleriaApi.Migrations
{
    [DbContext(typeof(DBContexto))]
    [Migration("20230712155702_VendedorOrdenesDeCompra")]
    partial class VendedorOrdenesDeCompra
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PapeleriaApi.Modelos.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.OrdenDeCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int?>("ClienteId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaYHora")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<string>("Folio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int?>("VendedorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("VendedorId");

                    b.ToTable("OrdenesDeCompra");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("CodigoDeBarras")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExistenciaEnBodega")
                        .HasColumnType("int");

                    b.Property<int>("ExistenciaEnVenta")
                        .HasColumnType("int");

                    b.Property<int?>("MarcaId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("MarcaId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Vendedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApelidoPaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Codigo")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Correo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vendedores");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.OrdenDeCompra", b =>
                {
                    b.HasOne("PapeleriaApi.Modelos.Cliente", "Cliente")
                        .WithMany("OrdenesDeCompra")
                        .HasForeignKey("ClienteId");

                    b.HasOne("PapeleriaApi.Modelos.Producto", "Producto")
                        .WithMany("OrdenesDeCompra")
                        .HasForeignKey("ProductoId");

                    b.HasOne("PapeleriaApi.Modelos.Vendedor", "Vendedor")
                        .WithMany("OrdenesDeCompra")
                        .HasForeignKey("VendedorId");

                    b.Navigation("Cliente");

                    b.Navigation("Producto");

                    b.Navigation("Vendedor");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Producto", b =>
                {
                    b.HasOne("PapeleriaApi.Modelos.Categoria", "Categoria")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaId");

                    b.HasOne("PapeleriaApi.Modelos.Marca", "Marca")
                        .WithMany("Productos")
                        .HasForeignKey("MarcaId");

                    b.Navigation("Categoria");

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Categoria", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Cliente", b =>
                {
                    b.Navigation("OrdenesDeCompra");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Marca", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Producto", b =>
                {
                    b.Navigation("OrdenesDeCompra");
                });

            modelBuilder.Entity("PapeleriaApi.Modelos.Vendedor", b =>
                {
                    b.Navigation("OrdenesDeCompra");
                });
#pragma warning restore 612, 618
        }
    }
}
