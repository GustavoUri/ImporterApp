using ImporterCore.Interfaces;
using ImporterDomain.Entities;

namespace ImporterCore.Core;

public class CsvConfigParser : ICsvParser
{
    public IConfiguration Parse(string fileContent)
    {
        var allData = fileContent.Split(';');

        var configuration = new Configuration()
        {
            Name = allData[0],
            Description = allData[1]
        };
        return configuration;
    }
}