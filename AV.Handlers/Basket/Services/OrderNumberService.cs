using AV.Common.Entities;
using AV.Common.Interfaces.Services;
using AV.Persistence.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AV.Handlers.Basket.Services
{
    public class OrderNumberService : IOrderNumberService
    {
        private readonly ValuationsContext _dbContext;

        private const string OrderNumberPrefixKey = "OrderNumberPrefix";
        private const string OrderNumberSeedValueKey = "OrderNumberSeedValue";
        private const string OrderNumberPaddingKey = "OrderNumberPaddingValue";

        public OrderNumberService(
            ValuationsContext dbContext
            )
        {
            _dbContext = dbContext;
        }

        public async Task<string> GenerateNextOrderNo()
        {
            var prefix = await GetPrefix();
            var padding = await GetPadding();
            var orderSeed = await GetSeed();
            
            //await _dbContext.SaveChangesAsync();
            
            return $"{prefix}{orderSeed.ToString().PadLeft(padding, '0')}";
        }

        private async Task<string> GetPrefix()
        {

            var prefixConfig = await _dbContext.SystemConfiguration
                .AsNoTracking()
                .Where(c => c.ItemName == OrderNumberPrefixKey)
                .FirstOrDefaultAsync();

            if (prefixConfig != null) 
                return prefixConfig.Value;

            prefixConfig = new SystemConfiguration(OrderNumberPrefixKey, "GSV-");
            _dbContext.SystemConfiguration.Update(prefixConfig);
            //await _dbContext.SaveChangesAsync();

            return prefixConfig.Value;
        }

        private async Task<int> GetPadding()
        {

            var paddingConfig = await _dbContext.SystemConfiguration
                .AsNoTracking()
                .Where(c => c.ItemName == OrderNumberPaddingKey)
                .FirstOrDefaultAsync();
            if (paddingConfig != null) 
                return int.Parse(paddingConfig.Value);
            
            paddingConfig = new SystemConfiguration(OrderNumberPaddingKey, 6.ToString());
            _dbContext.SystemConfiguration.Update(paddingConfig);
            //await _dbContext.SaveChangesAsync();

            return int.Parse(paddingConfig.Value);
        }

        private async Task<int> GetSeed()
        {

            var seedConfig = await _dbContext.SystemConfiguration
                .AsNoTracking()
                .Where(c => c.ItemName == OrderNumberSeedValueKey)
                .FirstOrDefaultAsync() 
                             ?? new SystemConfiguration(OrderNumberSeedValueKey, 0.ToString());

            seedConfig.Value = (int.Parse(seedConfig.Value) + 1).ToString();
            
            _dbContext.SystemConfiguration.Update(seedConfig);

            return int.Parse(seedConfig.Value);
        }
    }
}