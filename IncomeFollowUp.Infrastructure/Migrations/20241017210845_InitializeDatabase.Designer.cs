﻿// <auto-generated />
using System;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IncomeFollowUp.Infrastructure.Migrations
{
    [DbContext(typeof(IncomeFollowUpContext))]
    [Migration("20241017210845_InitializeDatabase")]
    partial class InitializeDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IncomeFollowUp.Domain.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DailyRate")
                        .HasColumnType("int");

                    b.Property<int>("ExpectedMonthlyIncome")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("IncomeFollowUp.Domain.WorkDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsWorkDay")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("WorkDays");
                });
#pragma warning restore 612, 618
        }
    }
}
