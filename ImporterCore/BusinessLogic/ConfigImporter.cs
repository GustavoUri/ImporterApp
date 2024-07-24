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

    public static List<Configuration> Configurations { get; set; } = new List<Configuration>();

    public void ImportConfigs(IEnumerable<FileInputModel> fileInputModels)
    {
        foreach (var fileInputModel in fileInputModels)
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

            lock (Configurations)
            {
                Configurations.Add(configuration);
            }
        }
    }

    public void ImportConfig(FileInputModel fileInputModel)
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

        lock (Configurations)
        {
            Configurations.Add(configuration);
        }
    }
}