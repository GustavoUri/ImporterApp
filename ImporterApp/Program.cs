using System.Diagnostics;
using ImporterCore.BusinessLogic;
using ImporterCore.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ImporterApp;

public static class Program
{
    static void Main()
    {
        var watch = new Stopwatch();
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfigImporter, ConfigImporter>()
            .AddScoped<IXmlParser, XmlConfigParser>()
            .AddScoped<ICsvParser, CsvParser>()
            .BuildServiceProvider();
        
        var files = Directory.GetFiles("../../../../FolderForReading");
        
        var fileContents = new List<FileInputModel>();
        
        Parallel.ForEach(files, (file) =>
        {
            
            var content = File.ReadAllText(file);
            var fileExtension = new FileInfo(file).Extension;
            var fileInputModel = new FileInputModel()
            {
                Content = content,
                Extension = fileExtension
            };
            lock (fileContents)
            {
                fileContents.Add(fileInputModel);
            }
        });
        
        var importerService = serviceProvider.GetService<IConfigImporter>();
        watch.Start();
        importerService.ImportConfigs(fileContents);
        
        // watch.Start();
        // Parallel.ForEach(files, (file) =>
        // {
        //     var content = File.ReadAllText(file);
        //     var fileExtension = new FileInfo(file).Extension;
        //     var fileInputModel = new FileInputModel()
        //     {
        //         Content = content,
        //         Extension = fileExtension
        //     };
        //     
        //     var importerService = serviceProvider.GetService<IConfigImporter>();
        //     importerService.ImportConfig(fileInputModel);
        //     
        // });
        
        var res = ConfigImporter.Configurations;
        foreach (var x in res)
        {
            Console.WriteLine(x.Name);
            Console.Write(x.Description);
            Console.WriteLine("\n");
        }
        
        watch.Stop();
        Console.WriteLine(watch.Elapsed);
    }
}