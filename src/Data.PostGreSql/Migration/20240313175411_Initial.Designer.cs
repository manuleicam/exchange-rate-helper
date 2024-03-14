﻿// <auto-generated />
using System;
using Data.PostGreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.PostGreSql.Migration
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240313175411_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.PostGreSql.Models.ExchangeRateSql", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<double>("AskPrice")
                        .HasColumnType("double precision")
                        .HasColumnName("ask_price");

                    b.Property<double>("BidPrice")
                        .HasColumnType("double precision")
                        .HasColumnName("bid_price");

                    b.Property<string>("FromCurrencyCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("from_currency_code");

                    b.Property<string>("FromCurrencyName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("from_currency_name");

                    b.Property<double>("Rate")
                        .HasColumnType("double precision")
                        .HasColumnName("rate");

                    b.Property<string>("ToCurrencyCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("to_currency_code");

                    b.Property<string>("ToCurrencyName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("to_currency_name");

                    b.HasKey("Id");

                    b.ToTable("exchangerates");
                });
#pragma warning restore 612, 618
        }
    }
}