using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Cell
{
  public int Id { get; set; }
  public int RowId { get; set; }
  public Row Row { get; set; }
  public int ColumnId { get; set; }
  public Column Column { get; set; }
  public string Value { get; set; }
  public string BackgroundColor { get; set; }
}
}