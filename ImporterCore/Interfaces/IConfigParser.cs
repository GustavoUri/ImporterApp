using ImporterDomain.Entities;

namespace ImporterCore.BusinessLogic;

public interface IConfigParser
{
    Configuration Parse(string fileContent);
}