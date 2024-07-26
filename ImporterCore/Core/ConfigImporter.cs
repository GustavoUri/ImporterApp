using ImporterCore.Interfaces;
using ImporterCore.Models;
using ImporterDomain.Entities;

namespace ImporterCore.Core;

public class ConfigImporter : IConfigImporter
{
    private readonly Dictionary<string, IConfigParser> _parsers;
    public ConfigImporter(IXmlParser xmlConfigParser, ICsvParser csvParser)
    {
        _parsers = new Dictionary<string, IConfigParser>
        {
            { ".csv", csvParser },
            { ".xml", xmlConfigParser }
        };
    }

    public IConfiguration ImportConfig(FileInputModel fileInputModel)
    {
        if (_parsers.TryGetValue(fileInputModel.Extension, out var parser))
        {
            return parser.Parse(fileInputModel.Content);
        }

        throw new NotSupportedException($"Unsupported file extension: {fileInputModel.Extension}");
    }
}