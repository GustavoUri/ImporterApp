using System.Xml.Linq;
using ImporterCore.Interfaces;
using ImporterDomain.Entities;

namespace ImporterCore.Core;

public class XmlConfigParser : IXmlParser
{
    public XmlConfigParser()
    {
    }

    public IConfiguration Parse(string fileContent)
    {
        var xDocument = XDocument.Parse(fileContent);
        var configElement = xDocument.Element("config"); 
        var name = configElement.Element("name");
        var description = configElement.Element("description");
        var config = new Configuration()
        {
            Name = name.Value,
            Description = description.Value
        };

        return config;
    }
}