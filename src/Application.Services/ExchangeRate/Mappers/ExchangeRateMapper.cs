namespace Application.Services.ExchangeRate.Mappers
{
    using Application.DTO.ExchangeRate;
    using Application.DTO.Gateways.CurrencyExchangeRate;
    using Application.Services.Currency.Mappers;
    using Domain.Model.Currency;
    using Domain.Model.ExchangeRate;

    public static class ExchangeRateMapper
    {
        public static ExchangeRate ToModel(this ExchangeRateDto exchangeRate)
        {
            if (exchangeRate == null)
            {
                return null;
            }
            
            return new ExchangeRate(
                (Guid) exchangeRate.Id,
                exchangeRate.FromCurrency.ToModel(),
                exchangeRate.ToCurrency.ToModel(),
                exchangeRate.Rate,
                exchangeRate.BidPrice,
                exchangeRate.AskPrice);
        }
        
        public static ExchangeRateDto ToDto(this ExchangeRate exchangeRate)
        {
            if (exchangeRate == null)
            {
                return null;
            }
            
            return new ExchangeRateDto()
            {
                Id = exchangeRate.Id,
                FromCurrency = exchangeRate.FromCurrency.ToDto(),
                ToCurrency = exchangeRate.ToCurrency.ToDto(),
                Rate = exchangeRate.Rate,
                BidPrice = exchangeRate.BidPrice,
                AskPrice = exchangeRate.AskPrice,
            };
        }

        public static ExchangeRate ToModel(this CurrencyExchangeRateDto currencyExchangeRateDto)
        {
            if (currencyExchangeRateDto == null)
            {
                return null;
            }

            var fromCurrency = new Currency(currencyExchangeRateDto.MetaData.FromCurrencyName,
                currencyExchangeRateDto.MetaData.FromCurrencyCode);
            var toCurrency = new Currency(currencyExchangeRateDto.MetaData.ToCurrencyName,
                currencyExchangeRateDto.MetaData.ToCurrencyCode);

            return new ExchangeRate(
                Guid.NewGuid(),
                fromCurrency,
                toCurrency,
                currencyExchangeRateDto.MetaData.ExchangeRate,
                currencyExchangeRateDto.MetaData.BidPrice,
                currencyExchangeRateDto.MetaData.AskPrice);
        }
    }
}