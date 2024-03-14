namespace Application.Services.Gateways
{
    using Domain.Model.ExchangeRate;

    public interface ICurrencyExchangeRateGateway
    {
        Task<ExchangeRate> GetExchangeRate(string fromCurrency, string toCurrency);
    }
}