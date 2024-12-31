﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Data;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(SpreadsheetDbContext))]
    partial class SpreadsheetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("api.Entities.Cell", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BackgroundColor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ColumnId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RowId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ColumnId");

                    b.HasIndex("RowId");

                    b.ToTable("Cells");
                });

            modelBuilder.Entity("api.Entities.Column", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpreadsheetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SpreadsheetId");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("api.Entities.Row", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpreadsheetId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SpreadsheetId");

                    b.ToTable("Rows");
                });

            modelBuilder.Entity("api.Entities.Spreadsheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Spreadsheets");
                });

            modelBuilder.Entity("api.Entities.Cell", b =>
                {
                    b.HasOne("api.Entities.Column", "Column")
                        .WithMany("Cells")
                        .HasForeignKey("ColumnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Entities.Row", "Row")
                        .WithMany("Cells")
                        .HasForeignKey("RowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Column");

                    b.Navigation("Row");
                });

            modelBuilder.Entity("api.Entities.Column", b =>
                {
                    b.HasOne("api.Entities.Spreadsheet", "Spreadsheet")
                        .WithMany("Columns")
                        .HasForeignKey("SpreadsheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spreadsheet");
                });

            modelBuilder.Entity("api.Entities.Row", b =>
                {
                    b.HasOne("api.Entities.Spreadsheet", "Spreadsheet")
                        .WithMany("Rows")
                        .HasForeignKey("SpreadsheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spreadsheet");
                });

            modelBuilder.Entity("api.Entities.Column", b =>
                {
                    b.Navigation("Cells");
                });

            modelBuilder.Entity("api.Entities.Row", b =>
                {
                    b.Navigation("Cells");
                });

            modelBuilder.Entity("api.Entities.Spreadsheet", b =>
                {
                    b.Navigation("Columns");

                    b.Navigation("Rows");
                });
#pragma warning restore 612, 618
        }
    }
}
