namespace Application.Services.Tests.ExchangeRate.Mappers
{
    using Application.DTO.Currency;
    using Application.DTO.ExchangeRate;
    using Application.DTO.Gateways.CurrencyExchangeRate;
    using Application.Services.ExchangeRate.Mappers;
    using Domain.Model.Currency;
    using Domain.Model.ExchangeRate;
    using Xunit;

    public class ExchangeRateMapperTests
    {
        [Fact]
        public void ToModel_ValidExchangeRate_ReturnExchangeRate()
        {
            // Arrange
            var currencyDtoFrom = new CurrencyDto()
            {
                Name = "United States",
                Code = "USD",
            };
            var currencyDtoTo = new CurrencyDto()
            {
                Name = "Euro",
                Code = "EUR",
            };

            var exchangeRateDto = new ExchangeRateDto()
            {
                Id = Guid.NewGuid(),
                FromCurrency = currencyDtoFrom,
                ToCurrency = currencyDtoTo,
                Rate = 1.1,
                AskPrice = 1.1,
                BidPrice = 1.1,
            };
            

            // Act
            var exchangeRate = exchangeRateDto.ToModel();
            
            // Assert
            Assert.NotNull(exchangeRate);
            Assert.IsType<ExchangeRate>(exchangeRate);
            
            Assert.Equal(exchangeRateDto.Id, exchangeRate.Id);
            Assert.Equal(exchangeRateDto.FromCurrency.Name, exchangeRate.FromCurrency.Name);
            Assert.Equal(exchangeRateDto.FromCurrency.Code, exchangeRate.FromCurrency.Code);
            Assert.Equal(exchangeRateDto.ToCurrency.Name, exchangeRate.ToCurrency.Name);
            Assert.Equal(exchangeRateDto.ToCurrency.Code, exchangeRate.ToCurrency.Code);
            
            Assert.Equal(exchangeRateDto.Rate, exchangeRate.Rate);
            Assert.Equal(exchangeRateDto.AskPrice, exchangeRate.AskPrice);
            Assert.Equal(exchangeRateDto.BidPrice, exchangeRate.BidPrice);
        }
        
        [Fact]
        public void ToModel_NullExchangeRateDto_ReturnNull()
        {
            // Arrange
            ExchangeRateDto exchangeRateDto = null;

            // Act
            var exchangeRate = exchangeRateDto.ToModel();
            
            // Assert
            Assert.Null(exchangeRate);
        }
        
        [Fact]
        public void ToDto_ValidExchangeRate_ReturnExchangeRate()
        {
            // Arrange
            var currencyFrom = new Currency("United States", "USD");

            var currencyTo = new Currency("Euro", "EUR");

            var exchangeRate = new ExchangeRate(Guid.NewGuid(), currencyFrom, currencyTo, 1.1, 1.1, 1.1);

            // Act
            var exchangeRateDto = exchangeRate.ToDto();
            
            // Assert
            Assert.NotNull(exchangeRateDto);
            Assert.IsType<ExchangeRateDto>(exchangeRateDto);
            
            Assert.Equal(exchangeRate.Id, exchangeRateDto.Id);
            Assert.Equal(exchangeRate.FromCurrency.Name, exchangeRateDto.FromCurrency.Name);
            Assert.Equal(exchangeRate.FromCurrency.Code, exchangeRateDto.FromCurrency.Code);
            Assert.Equal(exchangeRate.ToCurrency.Name, exchangeRateDto.ToCurrency.Name);
            Assert.Equal(exchangeRate.ToCurrency.Code, exchangeRateDto.ToCurrency.Code);
            
            Assert.Equal(exchangeRate.Rate, exchangeRateDto.Rate);
            Assert.Equal(exchangeRate.AskPrice, exchangeRateDto.AskPrice);
            Assert.Equal(exchangeRate.BidPrice, exchangeRateDto.BidPrice);
        }
        
        [Fact]
        public void ToDto_NullExchangeRate_ReturnNull()
        {
            // Arrange
            ExchangeRate exchangeRate = null;

            // Act
            var exchangeRateDto = exchangeRate.ToDto();
            
            // Assert
            Assert.Null(exchangeRateDto);
        }
        
        [Fact]
        public void ToModel_ValidCurrencyExchangeRateDto_ReturnExchangeRate()
        {
            // Arrange
            var metadata = new MetaData()
            {
                FromCurrencyCode = "USD",
                FromCurrencyName = "United States Dollar",
                ToCurrencyCode = "EUR",
                ToCurrencyName = "Euro",
                ExchangeRate = 1.1,
                LastRefreshed = "2020-01-01",
                TimeZone = "UTC",
                BidPrice = 1.1,
                AskPrice = 1.1,
            };
            
            var currencyExchangeRateDto = new CurrencyExchangeRateDto()
            {
                MetaData = metadata,
            };
            
            // Act
            var exchangeRate = currencyExchangeRateDto.ToModel();
            
            // Assert
            Assert.NotNull(exchangeRate);
            Assert.IsType<ExchangeRate>(exchangeRate);
            
            Assert.Equal(metadata.FromCurrencyName, exchangeRate.FromCurrency.Name);
            Assert.Equal(metadata.FromCurrencyCode, exchangeRate.FromCurrency.Code);
            Assert.Equal(metadata.ToCurrencyName, exchangeRate.ToCurrency.Name);
            Assert.Equal(metadata.ToCurrencyCode, exchangeRate.ToCurrency.Code);
            
            Assert.Equal(metadata.ExchangeRate, exchangeRate.Rate);
            Assert.Equal(metadata.AskPrice, exchangeRate.AskPrice);
            Assert.Equal(metadata.BidPrice, exchangeRate.BidPrice);
        }
        
        [Fact]
        public void ToModel_NullCurrencyExchangeRateDto_ReturnNull()
        {
            // Arrange
            CurrencyExchangeRateDto currencyExchangeRateDto = null;

            // Act
            var exchangeRate = currencyExchangeRateDto.ToModel();
            
            // Assert
            Assert.Null(exchangeRate);
        }
    }
}