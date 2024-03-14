namespace Data.PostGreSql.tests.Mappers
{

    using Data.PostGreSql.Mappers.ExchangeRate;
    using Data.PostGreSql.Models;
    using Domain.Model.Currency;
    using Domain.Model.ExchangeRate;
    using Xunit;

    public class ExchangeRateMapperTests
    {
        [Fact]
        public void ToModel_ValidExchangeRateSql_ReturnExchangeRate()
        {
            // Arrange
            var exchangeRateSql = new ExchangeRateSql()
            {
                Id = Guid.NewGuid(),
                FromCurrencyName = "United States",
                FromCurrencyCode = "USD",
                ToCurrencyName = "Euro",
                ToCurrencyCode = "EUR",
                Rate = 1.1,
                BidPrice = 1.1,
                AskPrice = 1.1,
            };

            // Act
            var exchangeRate = exchangeRateSql.ToModel();
            
            // Assert
            Assert.NotNull(exchangeRate);
            Assert.IsType<ExchangeRate>(exchangeRate);
            
            Assert.Equal(exchangeRateSql.Id, exchangeRate.Id);
            Assert.Equal(exchangeRateSql.FromCurrencyName, exchangeRate.FromCurrency.Name);
            Assert.Equal(exchangeRateSql.FromCurrencyCode, exchangeRate.FromCurrency.Code);
            Assert.Equal(exchangeRateSql.ToCurrencyName, exchangeRate.ToCurrency.Name);
            Assert.Equal(exchangeRateSql.ToCurrencyCode, exchangeRate.ToCurrency.Code);
            
            Assert.Equal(exchangeRateSql.Rate, exchangeRate.Rate);
            Assert.Equal(exchangeRateSql.AskPrice, exchangeRate.AskPrice);
            Assert.Equal(exchangeRateSql.BidPrice, exchangeRate.BidPrice);
        }
        
        [Fact]
        public void ToModel_NullExchangeRateSql_ReturnNull()
        {
            // Arrange
            ExchangeRateSql exchangeRateSql = null;

            // Act
            var exchangeRate = exchangeRateSql.ToModel();
            
            // Assert
            Assert.Null(exchangeRate);
        }
        
        [Fact]
        public void ToSql_ValidExchangeRate_ReturnExchangeRateSql()
        {
            // Arrange
            var currencyFrom = new Currency("United States", "USD");

            var currencyTo = new Currency("Euro", "EUR");

            var exchangeRate = new ExchangeRate(Guid.NewGuid(), currencyFrom, currencyTo, 1.1, 1.1, 1.1);

            // Act
            var exchangeRateSql = exchangeRate.ToSql();
            
            // Assert
            Assert.NotNull(exchangeRateSql);
            Assert.IsType<ExchangeRateSql>(exchangeRateSql);
            
            Assert.Equal(exchangeRate.Id, exchangeRateSql.Id);
            Assert.Equal(exchangeRate.FromCurrency.Name, exchangeRateSql.FromCurrencyName);
            Assert.Equal(exchangeRate.FromCurrency.Code, exchangeRateSql.FromCurrencyCode);
            Assert.Equal(exchangeRate.ToCurrency.Name, exchangeRateSql.ToCurrencyName);
            Assert.Equal(exchangeRate.ToCurrency.Code, exchangeRateSql.ToCurrencyCode);
            
            Assert.Equal(exchangeRate.Rate, exchangeRateSql.Rate);
            Assert.Equal(exchangeRate.AskPrice, exchangeRateSql.AskPrice);
            Assert.Equal(exchangeRate.BidPrice, exchangeRateSql.BidPrice);
        }
        
        [Fact]
        public void ToSql_NullExchangeRate_ReturnNull()
        {
            // Arrange
            ExchangeRate exchangeRate = null;

            // Act
            var exchangeRateSql = exchangeRate.ToSql();
            
            // Assert
            Assert.Null(exchangeRateSql);
        }
    }
}