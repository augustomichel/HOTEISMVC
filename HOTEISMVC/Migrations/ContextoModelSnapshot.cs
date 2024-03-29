﻿// <auto-generated />
using System;
using HOTEISMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HOTEISMVC.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HOTEISMVC.Models.Fotos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Id_Hotel")
                        .HasColumnType("int")
                        .HasColumnName("Id_Hotel");

                    b.Property<byte[]>("Img")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Img");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.HasIndex("Id_Hotel");

                    b.ToTable("fotos_hoteis");
                });

            modelBuilder.Entity("HOTEISMVC.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CNPJ");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Endereco");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.ToTable("hoteis");
                });

            modelBuilder.Entity("HOTEISMVC.Models.Quarto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Id_Hotel")
                        .HasColumnType("int")
                        .HasColumnName("Id_Hotel");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.Property<int>("NumAdultos")
                        .HasColumnType("int")
                        .HasColumnName("NumAdultos");

                    b.Property<int>("NumCriancas")
                        .HasColumnType("int")
                        .HasColumnName("NumCriancas");

                    b.Property<int>("NumOcupantes")
                        .HasColumnType("int")
                        .HasColumnName("NumOcupantes");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Preco");

                    b.HasKey("Id");

                    b.HasIndex("Id_Hotel");

                    b.ToTable("quartos");
                });

            modelBuilder.Entity("HOTEISMVC.Models.Fotos", b =>
                {
                    b.HasOne("HOTEISMVC.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("Id_Hotel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("HOTEISMVC.Models.Quarto", b =>
                {
                    b.HasOne("HOTEISMVC.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("Id_Hotel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hotel");
                });
#pragma warning restore 612, 618
        }
    }
}
