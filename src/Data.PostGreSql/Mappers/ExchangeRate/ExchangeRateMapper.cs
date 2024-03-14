namespace Data.PostGreSql.Mappers.ExchangeRate
{

    using Data.PostGreSql.Models;
    using Domain.Model.Currency;
    using Domain.Model.ExchangeRate;

    public static class ExchangeRateMapper
    {
        public static ExchangeRateSql ToSql(this ExchangeRate exchangeRate)
        {
            if (exchangeRate == null)
            {
                return null;
            }
            
            return new ExchangeRateSql()
            {
                Id = exchangeRate.Id,
                FromCurrencyName = exchangeRate.FromCurrency.Name,
                FromCurrencyCode = exchangeRate.FromCurrency.Code,
                ToCurrencyName = exchangeRate.ToCurrency.Name,
                ToCurrencyCode = exchangeRate.ToCurrency.Code,
                Rate = exchangeRate.Rate,
                BidPrice = exchangeRate.BidPrice,
                AskPrice = exchangeRate.AskPrice,
            };
        }
        
        public static ExchangeRate ToModel(this ExchangeRateSql exchangeRateSql)
        {
            if (exchangeRateSql == null)
            {
                return null;
            }

            var fromCurrency = new Currency(exchangeRateSql.FromCurrencyName, exchangeRateSql.FromCurrencyCode);
            var toCurrency = new Currency(exchangeRateSql.ToCurrencyName, exchangeRateSql.ToCurrencyCode);

            return new ExchangeRate(exchangeRateSql.Id, fromCurrency, toCurrency, exchangeRateSql.Rate, exchangeRateSql.BidPrice, exchangeRateSql.AskPrice);
        }
    }
}