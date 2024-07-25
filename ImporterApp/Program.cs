using System.Diagnostics;
using System.Text;
using ImporterCore.BusinessLogic;
using ImporterCore.Interfaces;
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

        watch.Start();
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
            importerService.ImportConfig(fileInputModel);
        });
        
        var res = ConfigImporter.Configurations;
        foreach (var x in res)
        {
            Console.WriteLine(x.Name);
            Console.WriteLine("\n");
        }
        
        watch.Stop();
        Console.WriteLine(watch.Elapsed);
    }

    public static string ReadText(string filePath)
    {
        using (var sourceStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
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
    }

}

