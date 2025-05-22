using Microsservices.Data;
using Microsservices.Entities;
using MongoDB.Driver;

namespace Microsservices.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;
        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }
        public async Task CreateProduct(Products product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Products> GetProduct(string id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Products>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter
                .Eq(p => p.Category, categoryName);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Products>> GetProductByName(string name)
        {
            FilterDefinition<Products> filter = Builders<Products>.Filter
                .Eq(p => p.Name, name);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Products>> GetProductsAsync()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Products product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(
                    filter: g => g.Id == product.Id, replacement: product
                );

            return updateResult.IsAcknowledged 
                && updateResult.ModifiedCount > 0;
        }
    }
}
