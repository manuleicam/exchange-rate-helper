namespace Application.Services.Tests.Currency.Mappers
{

    using Application.DTO.Currency;
    using Application.Services.Currency.Mappers;
    using Domain.Model.Currency;
    using Xunit;

    public class CurrencyMapperTests
    {
        [Fact]
        public void ToModel_ValidCurrency_ReturnCurrency()
        {
            // Arrange
            var currencyDto = new CurrencyDto()
            {
                Name = "United States",
                Code = "USD",
            };

            // Act
            var currency = currencyDto.ToModel();
            
            // Assert
            Assert.NotNull(currency);
            Assert.IsType<Currency>(currency);
            
            Assert.Equal(currencyDto.Name, currency.Name);
            Assert.Equal(currencyDto.Code, currency.Code);
        }
        
        [Fact]
        public void ToModel_NullCurrencyDto_ReturnNull()
        {
            // Arrange
            CurrencyDto currencyDto = null;

            // Act
            var currency = currencyDto.ToModel();
            
            // Assert
            Assert.Null(currency);
        }
        
        [Fact]
        public void ToDto_ValidCurrency_ReturnCurrencyDto()
        {
            // Arrange
            var currency = new Currency("United States", "USD");

            // Act
            var currencyDto = currency.ToDto();
            
            // Assert
            Assert.NotNull(currencyDto);
            Assert.IsType<CurrencyDto>(currencyDto);
            
            Assert.Equal(currency.Name, currencyDto.Name);
            Assert.Equal(currency.Code, currencyDto.Code);
        }
        
        [Fact]
        public void ToDto_NullCurrency_ReturnNull()
        {
            // Arrange
            Currency currency = null;

            // Act
            var currencyDto = currency.ToDto();
            
            // Assert
            Assert.Null(currencyDto);
        }
    }
}