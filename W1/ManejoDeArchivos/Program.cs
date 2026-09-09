using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

var salesFiles = FindFiles("stores");
double grandTotal = 0;
var salesDetails = new Dictionary<string, double>();

foreach (var file in salesFiles)
{
    string salesJson = File.ReadAllText(file);
    SalesData data = JsonConvert.DeserializeObject<SalesData>(salesJson);
    double total = data?.Total ?? 0;
    grandTotal += total;
    salesDetails.Add(file, total);
}

GenerateSalesSummaryReport("salesSummary.txt", grandTotal, salesDetails);

static IEnumerable<string> FindFiles(string folderName)
{
    List<string> salesFiles = new List<string>();
    var foundFiles = Directory.EnumerateFiles(folderName, "*", SearchOption.AllDirectories);
    foreach (var file in foundFiles)
    {
        if (file.EndsWith("sales.json"))
        {
            salesFiles.Add(file);
        }
    }
    return salesFiles;
}

static void GenerateSalesSummaryReport(string reportPath, double totalSales, Dictionary<string, double> salesDetails)
{
    StringBuilder report = new StringBuilder();
    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($" Total Sales: {totalSales:C}\n");
    report.AppendLine(" Details:");
    foreach (var sale in salesDetails)
    {
        report.AppendLine($"  {sale.Key}: {sale.Value:C}");
    }
    File.WriteAllText(reportPath, report.ToString());
}

public class SalesData { public double Total { get; set; } }
