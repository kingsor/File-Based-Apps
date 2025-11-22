#:package CsvHelper@33.1.0
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;
using System.Globalization;


var options = new JsonSerializerOptions
{
    TypeInfoResolver = SerializerContext.Default
};

var json = await File.ReadAllTextAsync("sales_data.json");
var sales = JsonSerializer.Deserialize<List<SaleRecord>>(json, options);

var topProducts = sales
    .GroupBy(s => s.Product)
    .Select(g => new {
        Product = g.Key,
        TotalRevenue = g.Sum(s => s.Amount),
        UnitsSold = g.Count()
    })
    .OrderByDescending(p => p.TotalRevenue)
    .Take(10);

using var writer = new StreamWriter("top_products.csv");
using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
csv.WriteRecords(topProducts);

Console.WriteLine("Report generated! Check top_products.csv");

public record SaleRecord(string Product, decimal Amount, DateTime Date);

[JsonSerializable(typeof(List<SaleRecord>))]
public partial class SerializerContext : JsonSerializerContext { }