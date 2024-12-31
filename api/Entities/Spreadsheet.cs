using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entities
{
    public class Spreadsheet
{
  public int Id { get; set; }
  public string Name { get; set; }
  public ICollection<Row> Rows { get; set; }
  public ICollection<Column> Columns { get; set; }
}
}