using ImporterDomain.Entities;

namespace ImporterCore.Interfaces;

public interface IConfigParser
{
    IConfiguration Parse(string fileContent);
}