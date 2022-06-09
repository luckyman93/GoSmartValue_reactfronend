using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AV.Common.Entities;
using AV.Common.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AV.Persistence.EntityFramework.Repositories
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        public ProductsRepository(ValuationsContext context) : base(context)
        {
        }

        public Task<Product> GetByName(string name, CancellationToken cancellationToken)
        {
            return DbContext.Set<Product>()
                .AsNoTracking()
                .Include(p => p.Features)
                .FirstOrDefaultAsync(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }
        public async Task<Product> GetProductById( int productId, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Product>().FindAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllWithFeatures( CancellationToken cancellationToken)
        {
            return await DbContext.Set<Product>()
                .AsNoTracking()
                .Include(p => p.Features)
                .ToListAsync(cancellationToken);
        }

    }
}