using ImporterDomain.Entities;

namespace ImporterCore.BusinessLogic;

public interface IImporter
{
    IConfiguration Import(string filePath);
}