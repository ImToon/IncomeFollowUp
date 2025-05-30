﻿// <auto-generated />
using System;
using IncomeFollowUp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IncomeFollowUp.Infrastructure.Migrations
{
    [DbContext(typeof(IncomeFollowUpContext))]
    partial class IncomeFollowUpContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("IncomeFollowUp.Domain.MonthlyIncome", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("ActualAmount")
                        .HasColumnType("int");

                    b.Property<int>("ExpectedAmount")
                        .HasColumnType("int");

                    b.Property<int>("Month")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MonthlyIncomes");
                });

            modelBuilder.Entity("IncomeFollowUp.Domain.MonthlyOutcome", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("MonthlyOutcomes");
                });

            modelBuilder.Entity("IncomeFollowUp.Domain.Settings", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("DailyRate")
                        .HasColumnType("int");

                    b.Property<int>("ExpectedMonthlyIncome")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Settings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d080520d-80dd-42cd-8353-828741f7ac37"),
                            DailyRate = 500,
                            ExpectedMonthlyIncome = 10000
                        });
                });

            modelBuilder.Entity("IncomeFollowUp.Domain.WorkDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("DailyRate")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsWorkDay")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("MonthlyIncomeId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MonthlyIncomeId");

                    b.ToTable("WorkDays");
                });

            modelBuilder.Entity("IncomeFollowUp.Domain.WorkDay", b =>
                {
                    b.HasOne("IncomeFollowUp.Domain.MonthlyIncome", "MonthlyIncome")
                        .WithMany("WorkDays")
                        .HasForeignKey("MonthlyIncomeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MonthlyIncome");
                });

            modelBuilder.Entity("IncomeFollowUp.Domain.MonthlyIncome", b =>
                {
                    b.Navigation("WorkDays");
                });
#pragma warning restore 612, 618
        }
    }
}
