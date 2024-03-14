namespace Application.Services.Gateways
{
    using Application.DTO.Gateways.CurrencyExchangeRate;
    using Application.Services.ExchangeRate.Mappers;
    using Domain.Model.ExchangeRate;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class CurrencyExchangeRateGateway : ICurrencyExchangeRateGateway
    {
        private readonly ILogger<CurrencyExchangeRateGateway> logger;
        
        public CurrencyExchangeRateGateway(ILogger<CurrencyExchangeRateGateway> logger)
        {
            this.logger = logger;
        }
        
        public async Task<ExchangeRate> GetExchangeRate(string fromCurrency, string toCurrency)
        {
            const string apiKey = "JOCZ4XTLKSTBCA8X";
            
            this.logger.LogInformation(
                null,
                $"Alphavantage external call used for FromCurrencyCode: {fromCurrency} and ToCurrencyCode: {toCurrency}",
                new
                {
                    FromCurrencyCode = fromCurrency,
                    ToCurrencyCode = toCurrency,
                });
            
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={fromCurrency}&to_currency={toCurrency}&apikey={apiKey}");
                var responseString = await response.Content.ReadAsStringAsync();
                
                var currencyExchangeRateDto = JsonConvert.DeserializeObject<CurrencyExchangeRateDto>(responseString);
                
                return currencyExchangeRateDto.ToModel();
            }
        }
    }
}