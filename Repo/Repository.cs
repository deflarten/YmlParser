using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using YmlParser.Models;

namespace YmlParser.Repo
{
    public class Repository : IRepository
    {
        private readonly ILogger logger;
        public Repository(ILogger<Repository> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public int AddProducts(IEnumerable<Product> products)
        {
            if (products == null)
                throw new ArgumentNullException(nameof(products));

            if (!products.Any())
            {
                logger.LogWarning($"Коллекция пустая, возврат из метода со значением 0. Добавление в репозиторий не произведено");
                return 0;
            }

            logger.LogDebug($"Для добавления в репозиторий передана коллекция объектов, количество: {products.Count()}");
            var context = new ProductDbContext();

            logger.LogDebug("Производится добавление объектов в репозиторий");
            context.Products.AddRange(products);
            logger.LogDebug("Добавление объектов в репозиторий завершено");

            logger.LogDebug("Производится сохранение изменений в БД");
            int numberOfWritten = context.SaveChanges();

            if (numberOfWritten != products.Count())
                logger.LogCritical($"Для записи в БД было передано {products.Count()}, а записано {numberOfWritten}");

            logger.LogDebug($"Сохранение изменений успешно завершено. Записано строк: {numberOfWritten}");
            return numberOfWritten;
        }

        public IEnumerable<Product> GetProducts(string shopId)
        {
            if (String.IsNullOrWhiteSpace(shopId))
                throw new ArgumentNullException(nameof(shopId));

            logger.LogDebug($"Метод GetProducts класса Repository начал работу со значением аргумента shopId = {shopId}");

            logger.LogDebug($"Инициализация экземпляра контекста данных");
            var context = new ProductDbContext();

            logger.LogDebug("Обращение к контексту");
            var products = context.Products.Where(i => i.ShopId == shopId);

            logger.LogDebug($"Продуктов с shopId = {shopId} найдено: {products.Count()}");
            logger.LogDebug("Запрос выполнен успешно, возврат из метода");
            return products;
        }
    }
}
