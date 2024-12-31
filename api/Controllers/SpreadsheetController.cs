using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Collections.Generic;
using System.Linq;
using api.Data;
using api.Entities;

[ApiController]
[Route("api/[controller]")]
public class SpreadsheetController : ControllerBase
{
  private readonly SpreadsheetDbContext _context;

  public SpreadsheetController(SpreadsheetDbContext context)
  {
    _context = context;
  }

  // GET api/spreadsheets
  [HttpGet]
  public IActionResult GetSpreadsheets()
  {
    var spreadsheets = _context.Spreadsheets.ToList();
    return Ok(spreadsheets);
  }

  // GET api/spreadsheets/{id}
  [HttpGet("{id}")]
  public IActionResult GetSpreadsheet(int id)
  {
    var spreadsheet = _context.Spreadsheets.Find(id);
    if (spreadsheet == null) return NotFound();
    return Ok(spreadsheet);
  }

  // POST api/spreadsheets
  [HttpPost]
  public IActionResult CreateSpreadsheet([FromBody] CreateSpreadsheetRequest request)
  {
    var spreadsheet = new Spreadsheet { Name = request.Name };
    _context.Spreadsheets.Add(spreadsheet);
    _context.SaveChanges();
    return Ok(spreadsheet);
  }

  // POST api/spreadsheets/{id}/rows
  [HttpPost("{id}/rows")]
  public IActionResult AddRow(int id)
  {
    var spreadsheet = _context.Spreadsheets.Find(id);
    if (spreadsheet == null) return NotFound();

    var newRow = new Row { SpreadsheetId = id };
    _context.Rows.Add(newRow);
    _context.SaveChanges();

    // Create new cells for the new row
    foreach (var column in spreadsheet.Columns)
    {
      var newCell = new Cell { RowId = newRow.Id, ColumnId = column.Id };
      _context.Cells.Add(newCell);
    }

    _context.SaveChanges();
    return Ok(newRow);
  }

  // POST api/spreadsheets/{id}/columns
  [HttpPost("{id}/columns")]
  public IActionResult AddColumn(int id)
  {
    var spreadsheet = _context.Spreadsheets.Find(id);
    if (spreadsheet == null) return NotFound();

    var newColumn = new Column { SpreadsheetId = id };
    _context.Columns.Add(newColumn);
    _context.SaveChanges();

    // Update existing cells to include the new column
    foreach (var row in spreadsheet.Rows)
    {
      foreach (var cell in row.Cells)
      {
        if (cell.ColumnId == null)
        {
          cell.ColumnId = newColumn.Id;
        }
      }
    }

    _context.SaveChanges();
    return Ok(newColumn);
  }

  // PUT api/spreadsheets/{id}/cells/{rowId}/{columnId}
  [HttpPut("{id}/cells/{rowId}/{columnId}")]
  public IActionResult EditCell(int id, int rowId, int columnId, [FromBody] EditCellRequest request)
  {
    var spreadsheet = _context.Spreadsheets.Find(id);
    if (spreadsheet == null) return NotFound();

    var cell = _context.Cells.Find(rowId, columnId);
    if (cell == null) return NotFound();

    cell.Value = request.Value;
    cell.BackgroundColor = request.BackgroundColor;
    _context.SaveChanges();
    return Ok(cell);
  }

  // DELETE api/spreadsheets/{id}
  [HttpDelete("{id}")]
  public IActionResult DeleteSpreadsheet(int id)
  {
    var spreadsheet = _context.Spreadsheets.Find(id);
    if (spreadsheet == null) return NotFound();

    _context.Spreadsheets.Remove(spreadsheet);
    _context.SaveChanges();
    return Ok();
  }
}

public class CreateSpreadsheetRequest
{
  public string Name { get; set; }
}

public class EditCellRequest
{
  public string Value { get; set; }
  public string BackgroundColor { get; set; }
}