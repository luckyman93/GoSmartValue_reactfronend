using System.Collections;
using System.Collections.Generic;
using AV.Common.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AV.Common.Interfaces
{
    public interface IProductsRepository : IRepository<Product>
    {
        Task<Product> GetByName(string name, CancellationToken cancellationToken);
        Task<Product> GetProductById(int productId, CancellationToken cancellationToken);
        Task<IEnumerable<Product>> GetAllWithFeatures(CancellationToken cancellationToken);
    }
}
