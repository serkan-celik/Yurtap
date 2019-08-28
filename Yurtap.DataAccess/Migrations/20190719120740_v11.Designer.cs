﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yurtap.DataAccess.Concrete.EntityFramework;

namespace Yurtap.DataAccess.Migrations
{
    [DbContext(typeof(YurtapDbContext))]
    [Migration("20190719120740_v11")]
    partial class v11
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Yurtap.Entity.KisiEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ad");

                    b.Property<byte>("Durum");

                    b.Property<DateTime>("EklemeTarihi");

                    b.Property<int?>("EkleyenId");

                    b.Property<DateTime?>("SonGuncellemeTarihi");

                    b.Property<int?>("SonGuncelleyenId");

                    b.Property<string>("Soyad");

                    b.Property<string>("TcKimlikNo");

                    b.HasKey("Id");

                    b.ToTable("Kisiler");
                });

            modelBuilder.Entity("Yurtap.Entity.KullaniciEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("KisiId");

                    b.Property<string>("Ad");

                    b.Property<byte>("Durum");

                    b.Property<DateTime>("EklemeTarihi");

                    b.Property<int?>("EkleyenId");

                    b.Property<string>("Sifre");

                    b.Property<DateTime?>("SonGuncellemeTarihi");

                    b.Property<int?>("SonGuncelleyenId");

                    b.HasKey("Id");

                    b.ToTable("Kullanicilar");
                });

            modelBuilder.Entity("Yurtap.Entity.OgrenciEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("KisiId");

                    b.Property<byte>("Durum");

                    b.Property<DateTime>("EklemeTarihi");

                    b.Property<int?>("EkleyenId");

                    b.Property<DateTime?>("SonGuncellemeTarihi");

                    b.Property<int?>("SonGuncelleyenId");

                    b.HasKey("Id");

                    b.ToTable("Ogrenciler");
                });

            modelBuilder.Entity("Yurtap.Entity.PersonelEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnName("KisiId");

                    b.Property<byte>("Durum");

                    b.Property<DateTime>("EklemeTarihi");

                    b.Property<int?>("EkleyenId");

                    b.Property<DateTime?>("SonGuncellemeTarihi");

                    b.Property<int?>("SonGuncelleyenId");

                    b.HasKey("Id");

                    b.ToTable("Personeller");
                });

            modelBuilder.Entity("Yurtap.Entity.YoklamaBaslikEntity", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Baslik");

                    b.Property<byte>("Durum");

                    b.Property<DateTime>("EklemeTarihi");

                    b.Property<int?>("EkleyenId");

                    b.Property<DateTime?>("SonGuncellemeTarihi");

                    b.Property<int?>("SonGuncelleyenId");

                    b.HasKey("Id");

                    b.ToTable("YoklamaBasliklari");
                });

            modelBuilder.Entity("Yurtap.Entity.YoklamaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Durum");

                    b.Property<DateTime>("EklemeTarihi");

                    b.Property<int?>("EkleyenId");

                    b.Property<string>("Liste");

                    b.Property<DateTime?>("SonGuncellemeTarihi");

                    b.Property<int?>("SonGuncelleyenId");

                    b.Property<DateTime>("Tarih");

                    b.Property<byte>("YoklamaBaslikId");

                    b.HasKey("Id");

                    b.ToTable("Yoklamalar");
                });
#pragma warning restore 612, 618
        }
    }
}
