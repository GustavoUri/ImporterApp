using ImporterDomain.Entities;

namespace ImporterCore.BusinessLogic;

public class CsvParser : ICsvParser
{
    public Configuration Parse(string fileContent)
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