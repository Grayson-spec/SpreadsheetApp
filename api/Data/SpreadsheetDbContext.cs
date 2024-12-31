using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace api.Data
{
    using api.Entities;
    using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

public class SpreadsheetDbContext : DbContext
{
  public DbSet<Spreadsheet> Spreadsheets { get; set; }
  public DbSet<Row> Rows { get; set; }
  public DbSet<Column> Columns { get; set; }
  public DbSet<Cell> Cells { get; set; }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseSqlite("Data Source=spreadsheet.db");
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Spreadsheet>()
      .HasMany(s => s.Rows)
      .WithOne(r => r.Spreadsheet)
      .HasForeignKey(r => r.SpreadsheetId);

    modelBuilder.Entity<Spreadsheet>()
      .HasMany(s => s.Columns)
      .WithOne(c => c.Spreadsheet)
      .HasForeignKey(c => c.SpreadsheetId);

    modelBuilder.Entity<Row>()
      .HasMany(r => r.Cells)
      .WithOne(c => c.Row)
      .HasForeignKey(c => c.RowId);

    modelBuilder.Entity<Column>()
      .HasMany(c => c.Cells)
      .WithOne(c => c.Column)
      .HasForeignKey(c => c.ColumnId);
  }
}
}