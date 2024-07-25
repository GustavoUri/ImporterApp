using ImporterCore.Models;
using ImporterDomain.Entities;

namespace ImporterCore.Interfaces;

public interface IConfigImporter
{
    IConfiguration ImportConfig(FileInputModel fileInputModel);
}