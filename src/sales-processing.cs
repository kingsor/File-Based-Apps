#:package CsvHelper@33.1.0

#:property PublishAot=false

using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;
using System.Globalization;

var salesFilename = "sales_data.json";

var json = await File.ReadAllTextAsync(salesFilename);
var sales = JsonSerializer.Deserialize<List<SaleRecord>>(json);

if(sales is not null)
{
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

}
else
{
    Console.WriteLine($"File {salesFilename} was empty. Unable to generate report!");
}


public record SaleRecord(string Product, decimal Amount, DateTime Date);

