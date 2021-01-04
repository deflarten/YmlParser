using System.ComponentModel.DataAnnotations;

namespace YmlParser.Models
{
    public class Product
    {
        // Уникальный ключ
        [Key]
        public int ProductId { get; set; }

        // Параметр id из yml-файла
        public int Id { get; set; }

        // Параметр name из yml-файла
        public string Name { get; set; }

        // Идентификатор магазина
        public string ShopId { get; set; }
    }
}
