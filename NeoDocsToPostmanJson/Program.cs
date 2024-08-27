using Microsoft.Toolkit.Parsers.Markdown;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Reflection.Emit;

namespace NeoDocsToPostmanJson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var defaultPath = string.Empty;
            if (args.Length == 1 && !string.IsNullOrEmpty(args[0])) 
            {
                defaultPath = args[0];
            }
            if (string.IsNullOrEmpty(defaultPath))
            {
                defaultPath = "D:\\1Code\\chen\\neo-dev-portal\\docs\\n3\\reference\\rpc";
            }
            if (!Directory.Exists(defaultPath))
            {
                Console.WriteLine("Please download neo-dev-portal project https://github.com/neo-project/neo-dev-portal\r\nand input the path of neo RPC docs, such as: D:\\neo-dev-portal\\n3\\reference\\rpc");
                defaultPath = Console.ReadLine();
            }

            var files = Directory.GetFiles(defaultPath, "*.md").ToList();
            files.RemoveAll(p => p.Contains("api.md"));

            var export = new Export();

            foreach (var file in files)
            {
                var document = new MarkdownDocument();
                document.Parse(File.ReadAllText(file));
                var title = document.Blocks.FirstOrDefault(p => p.Type == MarkdownBlockType.Header)?.ToString().Trim();
                var name = title.Split(' ')[0];
                var raw = document.Blocks.FirstOrDefault(p => p.Type == MarkdownBlockType.Code)?.ToString().Trim();
                var desc = "";
                foreach (var item in File.ReadAllLines(file).Skip(1))
                {
                    if (item.Contains("Example") || item.Contains("```")) break;
                    desc += item + "\r\n";
                }
                desc = desc.Replace(":::", string.Empty);
                export.Info = new Info();
                export.Item.Add(new Item()
                {
                    Name = name,
                    Request = new Request()
                    {
                        Body = new Body()
                        {
                            Raw = raw
                        },
                        Url = new URL(),
                        Description = desc
                    }
                });
            }

            var json = JsonConvert.SerializeObject(export, new JsonSerializerSettings{
                ContractResolver = new LowercaseContractResolver(),
                Formatting = Formatting.Indented
            });
            Console.WriteLine(json);
            var outputFileName = "RpcServer.postman_collection.json";
            File.WriteAllText(outputFileName, json);
            Console.WriteLine($"Save to {outputFileName}");
        }
    }
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
