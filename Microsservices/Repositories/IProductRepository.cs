using Microsservices.Entities;

namespace Microsservices.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Products>> GetProductsAsync();
        Task<Products> GetProduct(string id);
        Task<IEnumerable<Products>> GetProductByName(string name);
        Task<IEnumerable<Products>> GetProductByCategory(string categoryName);
        
        Task CreateProduct(Products product);
        Task<bool> UpdateProduct(Products product);
        Task<bool> DeleteProduct(string id);

    }
}
