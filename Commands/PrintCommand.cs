using Microsoft.Extensions.Logging;
using System;
using YmlParser.Repo;
using YmlParser.DataExport;
using System.Linq;

namespace YmlParser.Commands
{
    public class PrintCommand : ICommand
    {
        private readonly IRepository repository;
        private readonly IDataExport dataExport;
        private readonly ILogger<PrintCommand> logger;

        public PrintCommand(IRepository repository, IDataExport dataExport, ILogger<PrintCommand> logger)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.dataExport = dataExport ?? throw new ArgumentNullException(nameof(dataExport));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public CommandResult Execute(string[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));

            if (args.Length != 2)
            {
                string message = "Для команды print было передано недопустимое количество аргументов";
                logger.LogDebug($"{message}: {args.Length}");
                return new CommandResult(message);
            }

            string shopId = args[1];
            if (String.IsNullOrWhiteSpace(shopId))
            {
                logger.LogWarning($"Второй аргумент, shopId, имеет нулевую ссылку либо является пустой строкой");
                return new CommandResult($"Аргумент {nameof(shopId)} не может быть пустой строкой");
            }

            logger.LogDebug($"Обращение к репозиторию для получения списка продуктов магазина с shopId = {shopId}");
            var products = repository.GetProducts(shopId);
            logger.LogDebug($"Репозиторий вернул {products.Count()} продукта(-ов)");

            logger.LogDebug("Обращение к экспортёру данных для получения строки нужного формата");
            return new CommandResult(dataExport.GetString(products));
        }
    }
}
