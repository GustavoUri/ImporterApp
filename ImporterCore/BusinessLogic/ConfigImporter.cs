using ImporterCore.Interfaces;
using ImporterCore.Models;
using ImporterDomain.Entities;

namespace ImporterCore.BusinessLogic;

public class ConfigImporter : IConfigImporter
{
    private readonly IXmlParser _xmlConfigParser;
    private readonly ICsvParser _csvParser;

    public ConfigImporter(IXmlParser xmlConfigParser, ICsvParser csvParser)
    {
        _csvParser = csvParser;
        _xmlConfigParser = xmlConfigParser;
    }

    public IConfiguration ImportConfig(FileInputModel fileInputModel)
    {
        var configuration = new Configuration();
        switch (fileInputModel.Extension)
        {
            case ".csv":
                configuration = _csvParser.Parse(fileInputModel.Content);
                break;
            case ".xml":
                configuration = _xmlConfigParser.Parse(fileInputModel.Content);
                break;
        }

        return configuration;
    }
}