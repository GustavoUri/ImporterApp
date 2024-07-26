using ImporterCore.Core;
using Xunit;

namespace ImporterTests.Tests;

public class CsvConfigParserTests
{
    [Fact]
    public void ParseValidCsvReturnsCorrectConfiguration()
    {
        var csvContent = "Конфигурация 2;Описание Конфигурации 2";
        
        var parser = new CsvConfigParser();

        var config = parser.Parse(csvContent);

        Assert.NotNull(config);
        Assert.Equal("Конфигурация 2", config.Name);
        Assert.Equal("Описание Конфигурации 2", config.Description);
    }

    [Fact]
    public void ParseInvalidCsvThrowsException()
    {
        var invalidCsvContent = "Конфигурация 2";
        
        var parser = new CsvConfigParser();
        
        Assert.Throws<IndexOutOfRangeException>(() => parser.Parse(invalidCsvContent));
    }
}