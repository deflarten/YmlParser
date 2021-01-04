using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YmlParser.Models;
using YmlParser.Parser;
using YmlParser.Repo;
using YmlParser.Web;

namespace YmlParser.Commands
{
    public class SaveCommand : ICommand
    {
        readonly IYmlParser ymlParser;
        readonly IWebProvider webProvider;
        readonly IRepository repository;
        readonly ILogger<SaveCommand> logger;

        public SaveCommand(IYmlParser ymlParser, IWebProvider webProvider, IRepository repository, ILogger<SaveCommand> logger)
        {
            this.ymlParser = ymlParser ?? throw new ArgumentNullException(nameof(ymlParser));
            this.webProvider = webProvider ?? throw new ArgumentNullException(nameof(webProvider));
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public CommandResult Execute(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            if (args.Length != 3)
            {
                string message = $"Для команды {args[0]} передано недопустимое количество аргументов";
                logger.LogDebug($"{message}: {args.Length}");
                return new CommandResult(message);
            }

            string shopId = args[1];
            if (String.IsNullOrWhiteSpace(shopId))
            {
                logger.LogWarning($"Второй аргумент, shopId, имеет нулевую ссылку либо является пустой строкой");
                return new CommandResult($"Аргумент shopId не может быть пустой строкой");
            }

            string url = args[2];
            if (String.IsNullOrWhiteSpace(url))
            {
                logger.LogWarning($"Третий аргумент, url, имеет нулевую ссылку либо является пустой строкой");
                return new CommandResult($"Аргумент url не может быть пустой строкой");
            }

            string filename = Path.GetTempFileName();
            try
            {
                logger.LogDebug($"Загрузка файла из {url} в {filename}");
                webProvider.DownloadFile(url, filename);

                logger.LogDebug($"Парсинг файла с присвоением продуктам shopId = {shopId}");
                IEnumerable<Product> products = ymlParser.Parse(filename, shopId);

                logger.LogDebug($"Добавление в репозиторий {products.Count()} продукта(-ов)");
                repository.AddProducts(products);
                return new CommandResult("Данные успешно записаны");
            }
            finally
            {
                if (File.Exists(filename)) File.Delete(filename);
            }
        }
    }
}
