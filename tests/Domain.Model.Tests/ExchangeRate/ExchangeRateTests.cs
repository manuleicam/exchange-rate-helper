namespace Domain.Model.Tests.ExchangeRate
{
    using Domain.Model.Currency;
    using Domain.Model.ExchangeRate;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using Xunit;

    public class ExchangeRateTests
    {
        [Fact]
        public void NewExchangeRate_AllValid_ReturnExchangeRate()
        {
            // Arrange
            const string expectedFromCurrencyName = "United States Dollar";
            const string expectedFromCurrencyCode = "USD";
            var fromCurrency = new Currency(expectedFromCurrencyName, expectedFromCurrencyCode);
            
            const string expectedToCurrencyName = "Euro";
            const string expectedToCurrencyCode = "EUR";
            var toCurrency = new Currency(expectedToCurrencyName, expectedToCurrencyCode);

            const double rate = 1.1;
            const double bidPrice = 1.1;
            const double askPrice = 1.1;
            var id = Guid.NewGuid();
            
            // Act
            var exchangeRate = new ExchangeRate(id, fromCurrency, toCurrency, rate, bidPrice, askPrice);    
            
            // Assert
            Assert.NotNull(exchangeRate);
            
            Assert.Equal(id, exchangeRate.Id);
            
            Assert.Equal(fromCurrency.Name, exchangeRate.FromCurrency.Name);
            Assert.Equal(fromCurrency.Code, exchangeRate.FromCurrency.Code);
            
            Assert.Equal(toCurrency.Name, exchangeRate.ToCurrency.Name);
            Assert.Equal(toCurrency.Code, exchangeRate.ToCurrency.Code);
            
            Assert.Equal(rate, exchangeRate.Rate);
            Assert.Equal(bidPrice, exchangeRate.BidPrice);
            Assert.Equal(askPrice, exchangeRate.AskPrice);
        }
        
        [Fact]
        public void NewExchangeRate_NegativeRate_ThrowsDomainModelException()
        {
            // Arrange
            const string expectedFromCurrencyName = "United States Dollar";
            const string expectedFromCurrencyCode = "USD";
            var fromCurrency = new Currency(expectedFromCurrencyName, expectedFromCurrencyCode);
            
            const string expectedToCurrencyName = "Euro";
            const string expectedToCurrencyCode = "EUR";
            var toCurrency = new Currency(expectedToCurrencyName, expectedToCurrencyCode);

            const double rate = -1.1;
            const double bidPrice = 1.1;
            const double askPrice = 1.1;
            
            var id = Guid.NewGuid();
            // Act
            Action act = () => new ExchangeRate(id, fromCurrency, toCurrency, rate, bidPrice, askPrice);
            
            // Assert
            var exception = Assert.Throws<DomainModelException>(act);
            Assert.Equal(ErrorMessages.ExchangeRateRateInvalid, exception.Message);
        }
        
        [Fact]
        public void NewExchangeRate_NegativeBidPrice_ThrowsDomainModelException()
        {
            // Arrange
            const string expectedFromCurrencyName = "United States Dollar";
            const string expectedFromCurrencyCode = "USD";
            var fromCurrency = new Currency(expectedFromCurrencyName, expectedFromCurrencyCode);
            
            const string expectedToCurrencyName = "Euro";
            const string expectedToCurrencyCode = "EUR";
            var toCurrency = new Currency(expectedToCurrencyName, expectedToCurrencyCode);

            const double rate = 1.1;
            const double bidPrice = -1.1;
            const double askPrice = 1.1;
            var id = Guid.NewGuid();
            
            // Act
            Action act = () => new ExchangeRate(id, fromCurrency, toCurrency, rate, bidPrice, askPrice);
            
            // Assert
            var exception = Assert.Throws<DomainModelException>(act);
            Assert.Equal(ErrorMessages.ExchangeRateBidPriceInvalid, exception.Message);
        }
        
        [Fact]
        public void NewExchangeRate_NegativeAskPrice_ThrowsDomainModelException()
        {
            // Arrange
            const string expectedFromCurrencyName = "United States Dollar";
            const string expectedFromCurrencyCode = "USD";
            var fromCurrency = new Currency(expectedFromCurrencyName, expectedFromCurrencyCode);
            
            const string expectedToCurrencyName = "Euro";
            const string expectedToCurrencyCode = "EUR";
            var toCurrency = new Currency(expectedToCurrencyName, expectedToCurrencyCode);

            const double rate = 1.1;
            const double bidPrice = 1.1;
            const double askPrice = -1.1;
            var id = Guid.NewGuid();
            
            // Act
            Action act = () => new ExchangeRate(id, fromCurrency, toCurrency, rate, bidPrice, askPrice);
            
            // Assert
            var exception = Assert.Throws<DomainModelException>(act);
            Assert.Equal(ErrorMessages.ExchangeRateAskPriceInvalid, exception.Message);
        }
        
        [Fact]
        public void NewExchangeRate_FromCurrencyAndToCurrencyTheSame_ThrowsDomainModelException()
        {
            // Arrange
            const string expectedFromCurrencyName = "United States Dollar";
            const string expectedFromCurrencyCode = "USD";
            var fromCurrency = new Currency(expectedFromCurrencyName, expectedFromCurrencyCode);
            var toCurrency = new Currency(expectedFromCurrencyName, expectedFromCurrencyCode);

            const double rate = 1.1;
            const double bidPrice = 1.1;
            const double askPrice = -1.1;
            var id = Guid.NewGuid();
            
            // Act
            Action act = () => new ExchangeRate(id, fromCurrency, toCurrency, rate, bidPrice, askPrice);
            
            // Assert
            var exception = Assert.Throws<DomainModelException>(act);
            Assert.Equal(ErrorMessages.ExchangeRateSameCurrency, exception.Message);
        }
        
        [Fact]
        public void AreEqualCurrencies_AtLeastOneDifferent_ReturnFalse()
        {
            // Arrange
            var fromCurrency1 = BuildCurrency("NAME11", "CODE11");
            var toCurrency1 = BuildCurrency("NAME12", "CODE12");
            
            var fromCurrency2 = BuildCurrency("NAME21", "CODE21");
            var toCurrency2 = BuildCurrency("NAME22", "CODE22");
 
            const double rate = 1.1;
            const double bidPrice = 1.1;
            const double askPrice = 1.1;
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            
            var exchangeRate1 = new ExchangeRate(id1, fromCurrency1, toCurrency1, rate, bidPrice, askPrice);  
            var exchangeRate2 = new ExchangeRate(id2, fromCurrency2, toCurrency2, rate, bidPrice, askPrice);  
            
            // Act
            var areEqual = exchangeRate1.AreEqualCurrencies(exchangeRate2);
            
            // Assert
            Assert.False(areEqual);
        }
        
        [Fact]
        public void AreEqualCurrencies_AllTheSame_ReturnTrue()
        {
            // Arrange
            var fromCurrency1 = BuildCurrency("UnitedStates", "USD");
            var toCurrency1 = BuildCurrency("Euro", "EUR");
            
            var fromCurrency2 = BuildCurrency("UnitedStates", "USD");
            var toCurrency2 = BuildCurrency("Euro", "EUR");
 
            const double rate = 1.1;
            const double bidPrice = 1.1;
            const double askPrice = 1.1;
            var id = Guid.NewGuid();
            
            var exchangeRate1 = new ExchangeRate(id, fromCurrency1, toCurrency1, rate, bidPrice, askPrice);  
            var exchangeRate2 = new ExchangeRate(id, fromCurrency2, toCurrency2, rate, bidPrice, askPrice);  
            
            // Act
            var areEqual = exchangeRate1.AreEqualCurrencies(exchangeRate2);
            
            // Assert
            Assert.True(areEqual);
        }

        private static Currency BuildCurrency(string name, string code)
        {
            return new Currency(name, code);
        }
    }
}