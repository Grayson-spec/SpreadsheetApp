using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Row
{
  public int Id { get; set; }
  public int SpreadsheetId { get; set; }
  public Spreadsheet Spreadsheet { get; set; }
  public ICollection<Cell> Cells { get; set; }
}
}