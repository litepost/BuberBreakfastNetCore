﻿// <auto-generated />
using System;
using BuberBreakfast.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuberBreakfast.Migrations
{
    [DbContext(typeof(BuberBreakfastDbContext))]
    [Migration("20240101071039_buberMigration")]
    partial class buberMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("BuberBreakfast.Models.Breakfast", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Savory")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Sweet")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Breakfasts");
                });
#pragma warning restore 612, 618
        }
    }
}
