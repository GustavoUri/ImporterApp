using ImporterCore.Models;
using ImporterDomain.Entities;

namespace ImporterCore.BusinessLogic;

public interface IConfigImporter
{ 
    void ImportConfigs(IEnumerable<FileInputModel> fileInputModels);

    void ImportConfig(FileInputModel fileInputModel);
}