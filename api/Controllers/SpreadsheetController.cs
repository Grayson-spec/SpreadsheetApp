// Controllers/DuplicateFinderController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DuplicateFinderController : ControllerBase
    {
        private readonly string _connectionString;

        public DuplicateFinderController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost]
        public IActionResult FindDuplicates([FromBody]List<string> values)
        {
            try
            {
                using (var connection = new SqliteConnection(_connectionString))
                {
                    connection.Open();

                    // Create table if it doesn't exist
                    CreateTable(connection);

                    // Insert values into table
                    InsertValues(connection, values);

                    // Find duplicates in the table
                    var duplicates = FindDuplicates(connection);

                    return Ok(duplicates);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private void CreateTable(SqliteConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Values (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Value TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();
        }

        private void InsertValues(SqliteConnection connection, List<string> values)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Values (Value) VALUES (@Value);
            ";
            command.Parameters.AddWithValue("@Value", "");

            foreach (var value in values)
            {
                command.Parameters["@Value"].Value = value;
                command.ExecuteNonQuery();
            }
        }

        private List<string> FindDuplicates(SqliteConnection connection)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT Value
                FROM Values
                GROUP BY Value
                HAVING COUNT(Value) > 1;
            ";
            var reader = command.ExecuteReader();

            var duplicates = new List<string>();
            while (reader.Read())
            {
                duplicates.Add(reader["Value"].ToString());
            }

            return duplicates;
        }
    }
}