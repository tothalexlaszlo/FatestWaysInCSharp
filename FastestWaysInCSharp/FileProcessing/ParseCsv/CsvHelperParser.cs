using CsvHelper;
using CsvHelper.Configuration;
using FastestWaysInCSharp.FileProcessing.ParseCsv.Model;
using System.Globalization;

namespace FastestWaysInCSharp.FileProcessing.ParseCsv;

public static class CsvHelperParser
{
    private static readonly CsvConfiguration _csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        Delimiter = ";",
        NewLine = "\n"
    };

    public static IEnumerable<FakeName> Parse(string filePath)
    {
        using var streamReader = new StreamReader(filePath);
        using var csvReader = new CsvReader(streamReader, _csvConfiguration);
        foreach (var fakeName in csvReader.GetRecords<FakeName>())
        {
            yield return fakeName;
        }
    }

    public static async IAsyncEnumerable<FakeName> ParseAsync(string filePath)
    {
        using var streamReader = new StreamReader(filePath);
        using var csvReader = new CsvReader(streamReader, _csvConfiguration);
        await foreach (var fakeName in csvReader.GetRecordsAsync<FakeName>())
        {
            yield return fakeName;
        }
    }
}