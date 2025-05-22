using Microsservices.Entities;
using MongoDB.Driver;

namespace Microsservices.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Products> Products { get; }
    }
}
