using System.Xml.Linq;
using ImporterDomain.Entities;

namespace ImporterCore.BusinessLogic;

public class XmlImport : IImporter
{
    public XmlImport()
    {
    }

    public IConfiguration Import(string filePath)
    {
        var res = XDocument.Load(filePath);
        throw new NotImplementedException();
    }
}