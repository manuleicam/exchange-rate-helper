namespace Domain.Model.Tests.Currency
{
    using Domain.Model.Currency;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using Xunit;

    public class CurrencyTests
    {
        [Fact]
        public void NewCurrency_AllValid_ReturnCurrency()
        {
            // Arrange
            const string expectedName = "United States Dollar";
            const string expectedCode = "USD";
            
            // Act
            var currency = new Currency(expectedName, expectedCode);
            
            // Assert
            Assert.NotNull(currency);
            
            Assert.Equal(expectedName, currency.Name);
            Assert.Equal(expectedCode, currency.Code);
        }
        
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void NewCurrency_InvalidName_ThrowsDomainModelException(string name)
        {
            // Arrange
            const string expectedCode = "USD";
            
            // Act
            Action act = () => new Currency(name, expectedCode);
            
            // Assert
            var exception = Assert.Throws<DomainModelException>(act);
            Assert.Equal(ErrorMessages.CurrencyNameInvalid, exception.Message);
        }
        
        [Fact]
        public void NewCurrency_NameTooBig_ThrowsDomainModelException()
        {
            // Arrange
            var name = new string('B', Constants.CurrencyNameMaxLength + 1);
            const string expectedCode = "USD";
            
            // Act
            Action act = () => new Currency(name, expectedCode);
            
            // Assert
            var exception = Assert.Throws<DomainModelException>(act);
            Assert.Equal(ErrorMessages.CurrencyNameInvalid, exception.Message);
        }
        
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void NewCurrency_InvalidCode_ThrowsDomainModelException(string code)
        {
            // Arrange
            const string expectedName = "United States Dollar";
            
            // Act
            Action act = () => new Currency(expectedName, code);
            
            // Assert
            var exception = Assert.Throws<DomainModelException>(act);
            Assert.Equal(ErrorMessages.CurrencyCodeInvalid, exception.Message);
        }
        
        [Fact]
        public void NewCurrency_CodeTooBig_ThrowsDomainModelException()
        {
            // Arrange
            var code = new string('B', Constants.CurrencyNameMaxLength + 1);
            const string expectedName = "United States Dollar";
            
            // Act
            Action act = () => new Currency(expectedName, code);
            
            // Assert
            var exception = Assert.Throws<DomainModelException>(act);
            Assert.Equal(ErrorMessages.CurrencyCodeInvalid, exception.Message);
        }
    }
}