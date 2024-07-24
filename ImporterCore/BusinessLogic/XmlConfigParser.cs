using System.Xml.Linq;
using ImporterDomain.Entities;

namespace ImporterCore.BusinessLogic;

public class XmlConfigParser : IXmlParser
{
    public XmlConfigParser()
    {
    }

    public Configuration Parse(string fileContent)
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