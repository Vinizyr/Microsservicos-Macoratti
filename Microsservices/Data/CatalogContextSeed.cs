using Microsservices.Entities;
using MongoDB.Driver;

namespace Microsservices.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();

            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetMyProducts());
            }
        }

        private static IEnumerable<Product> GetMyProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = "1",
                    Name = "Mouse Gamer RGB",
                    Category = "Periféricos",
                    Description = "Mouse gamer com 7 botões programáveis e iluminação RGB.",
                    Image = "mouse.png",
                    Price = 199.90m
                },
                new Product
                {
                    Id = "2",
                    Name = "Teclado Mecânico",
                    Category = "Periféricos",
                    Description = "Teclado mecânico com switches azuis e retroiluminação.",
                    Image = "teclado.png",
                    Price = 349.99m
                },
                new Product
                {
                    Id = "3",
                    Name = "Monitor 27' 144Hz",
                    Category = "Monitores",
                    Description = "Monitor gamer de 27 polegadas com taxa de atualização de 144Hz.",
                    Image = "monitor.png",
                    Price = 1499.00m
                },
                new Product
                {
                    Id = "4",
                    Name = "Headset Gamer 7.1",
                    Category = "Periféricos",
                    Description = "Headset gamer com som surround 7.1 e cancelamento de ruído.",
                    Image = "headset.png",
                    Price = 299.90m
                },
                new Product
                {
                    Id = "5",
                    Name = "Placa de Vídeo RTX 4060",
                    Category = "Hardware",
                    Description = "Placa de vídeo NVIDIA RTX 4060 com 8GB de memória GDDR6.",
                    Image = "placa-video.png",
                    Price = 2499.99m
                }
            };
        }

    }
}
