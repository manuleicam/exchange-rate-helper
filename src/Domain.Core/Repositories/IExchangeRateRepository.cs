namespace Domain.Core.Repositories
{
    using Domain.Model.ExchangeRate;

    public interface IExchangeRateRepository
    {
        public Task<IEnumerable<ExchangeRate>> GetAll();

        public Task<ExchangeRate> GetExchangeRate(Guid id);

        public Task<IEnumerable<ExchangeRate>> GetExchangeRateByCurrency(string? fromCurrencyCode = default, string? toCurrencyCode = default);

        public Task<ExchangeRate> InsertExchangeRate(ExchangeRate exchangeRate);

        public Task UpdateExchangeRate(Guid exchangeRateId, ExchangeRate newExchangeRate);
        
        public Task DeleteExchangeRate(Guid id);
    }
}