namespace Data.PostGreSql.Repositories
{

    using Data.PostGreSql.Mappers.ExchangeRate;
    using Domain.Core.Repositories;
    using Domain.Model.ExchangeRate;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using Microsoft.EntityFrameworkCore;

    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly AppDbContext appDbContext;

        public ExchangeRateRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        
        public async Task<IEnumerable<ExchangeRate>> GetAll()
        {
            var exchangeRates = await appDbContext.ExchangeRates.ToListAsync();
            
            return exchangeRates.Select(er => er.ToModel());
        }

        public async Task<ExchangeRate> GetExchangeRate(Guid id)
        {
            var exchangeRate = await appDbContext.ExchangeRates.FindAsync(id);
            
            return exchangeRate.ToModel();
        }
        
        public async Task<IEnumerable<ExchangeRate>> GetExchangeRateByCurrency(string? fromCurrencyCode = default, string? toCurrencyCode = default)
        {
            var query = appDbContext.ExchangeRates.AsQueryable();

            if (!string.IsNullOrWhiteSpace(fromCurrencyCode))
            {
                query = query.Where(er => er.FromCurrencyCode == fromCurrencyCode);
            }
            
            if (!string.IsNullOrWhiteSpace(toCurrencyCode))
            {
                query = query.Where(er => er.ToCurrencyCode == toCurrencyCode);
            }

            var exchangeRates = await query.ToListAsync();
            
            return exchangeRates.Select(er => er.ToModel());
        }

        public async Task<ExchangeRate> InsertExchangeRate(ExchangeRate exchangeRate)
        {
            await appDbContext.ExchangeRates.AddAsync(exchangeRate.ToSql());
            await appDbContext.SaveChangesAsync();
            
            return exchangeRate;
        }

        public async Task UpdateExchangeRate(Guid exchangeRateId, ExchangeRate newExchangeRate)
        {
            var exchangeRate = await appDbContext.ExchangeRates.FindAsync(exchangeRateId);

            if (exchangeRate == null)
            {
                throw new ResourceNotFound(ErrorMessages.ExchangeRateNotFound);
            }
            
            exchangeRate.Rate = newExchangeRate.Rate;
            exchangeRate.AskPrice = newExchangeRate.AskPrice;
            exchangeRate.BidPrice = newExchangeRate.BidPrice;

            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteExchangeRate(Guid id)
        {
            var exchangeRate = await appDbContext.ExchangeRates.FindAsync(id);
            appDbContext.Entry(exchangeRate).State = EntityState.Deleted;
            
            await appDbContext.SaveChangesAsync();
        }
    }
}