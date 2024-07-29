using System.Text;
using ImporterCore.Core;
using ImporterCore.Interfaces;
using ImporterCore.Models;
using ImporterDomain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ImporterApp;

public static class Program
{
    public static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddScoped<IConfigImporter, ConfigImporter>()
            .AddScoped<IXmlParser, XmlConfigParser>()
            .AddScoped<ICsvParser, CsvConfigParser>()
            .BuildServiceProvider();

        var files = Directory.GetFiles("../../../../FolderForReading");

        var configurations = new List<IConfiguration>();

        Parallel.ForEach(files, (file) =>
        {
            var content = ReadText(file);

            var fileExtension = new FileInfo(file).Extension;
            var fileInputModel = new FileInputModel()
            {
                Content = content,
                Extension = fileExtension
            };

            var importerService = serviceProvider.GetService<IConfigImporter>();
            
            lock (configurations)
            {
                configurations.Add(importerService.ImportConfig(fileInputModel));
            }
        });

        WriteConfigNames(configurations);
    }

    private static string ReadText(string filePath)
    {
        using var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        
        var sb = new StringBuilder();
        var buffer = new byte[4096];
        int numRead;

        while ((numRead = sourceStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            var text = Encoding.UTF8.GetString(buffer, 0, numRead);
            sb.Append(text);
        }

        return sb.ToString();
    }

    private static void WriteConfigNames(List<IConfiguration> configs)
    {
        foreach (var config in configs)
        {
            Console.WriteLine($" Name: {config.Name}");
            Console.WriteLine($" Description: {config.Description}");
            Console.WriteLine("\n");
        }
    }
}