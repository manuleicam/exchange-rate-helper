namespace Application.Services.Tests.ExchangeRate.Commands
{

    using Application.DTO.Currency;
    using Application.DTO.ExchangeRate;
    using Application.Services.ExchangeRate.Commands;
    using FluentValidation.TestHelper;
    using Infrastructure.CrossCutting.Utils;
    using Xunit;

    public class UpdateExchangeRateValidatorTests
    {
        private readonly UpdateExchangeRateValidator updateExchangeRateValidator;

        public UpdateExchangeRateValidatorTests()
        {
            this.updateExchangeRateValidator = new UpdateExchangeRateValidator();
        }
        
        [Fact]
        public void Validator_AllValid_NoValidationErrors()
        {
            // Arrange
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = "code"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "toCurrencyName", 
                Code = "code"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id =Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1,
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }
        
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void Validator_InvalidFromCurrencyName_ValidationHasErrors(string fromCurrencyName)
        {
            // Arrange
            var fromCurrency = new CurrencyDto()
            {
                Name = fromCurrencyName, 
                Code = "USD"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "Euro", 
                Code = "EUR"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id = Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.CreateExchangesRateCurrencyNameInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Fact]
        public void Validator_FromCurrencyNameTooBig_ValidationHasErrors()
        {
            // Arrange
            var fromCurrencyName = new string('B', Constants.CurrencyNameMaxLength + 1);
            var fromCurrency = new CurrencyDto()
            {
                Name = fromCurrencyName, 
                Code = "USD"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "Euro", 
                Code = "EUR"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id =Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.CreateExchangesRateCurrencyNameInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void Validator_InvalidFromCurrencyCode_ValidationHasErrors(string fromCurrencyCode)
        {
            // Arrange
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = fromCurrencyCode
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "Euro", 
                Code = "EUR"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id = Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.CreateExchangesRateCurrencyCodeInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Fact]
        public void Validator_FromCurrencyCodeTooBig_ValidationHasErrors()
        {
            // Arrange
            var fromCurrencyCode = new string('B', Constants.CurrencyNameMaxLength + 1);
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = fromCurrencyCode
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "Euro", 
                Code = "EUR"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id =Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.CreateExchangesRateCurrencyCodeInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void Validator_InvalidToCurrencyName_ValidationHasErrors(string toCurrencyName)
        {
            // Arrange
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = "code"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = toCurrencyName,
                Code = "EUR"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id = Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.CreateExchangesRateCurrencyNameInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Fact]
        public void Validator_ToCurrencyNameTooBig_ValidationHasErrors()
        {
            // Arrange
            var toCurrencyName = new string('B', Constants.CurrencyNameMaxLength + 1);
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = "code"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = toCurrencyName, 
                Code = "EUR"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id =Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.CreateExchangesRateCurrencyNameInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void Validator_InvalidToCurrencyCode_ValidationHasErrors(string toCurrencyCode)
        {
            // Arrange
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = "code"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "toCurrencyName",
                Code = toCurrencyCode
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id = Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.CreateExchangesRateCurrencyCodeInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Fact]
        public void Validator_ToCurrencyCodeTooBig_ValidationHasErrors()
        {
            // Arrange
            var toCurrencyCode = new string('B', Constants.CurrencyNameMaxLength + 1);
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = "code"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "toCurrencyName", 
                Code = toCurrencyCode
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id =Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.CreateExchangesRateCurrencyCodeInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Fact]
        public void Validator_NegativeRate_ValidationHasErrors()
        {
            // Arrange
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = "code"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "toCurrencyName", 
                Code = "code"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id =Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = -1.1,
                AskPrice = 1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.ExchangeRateRateInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Fact]
        public void Validator_NegativeBidPrice_ValidationHasErrors()
        {
            // Arrange
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = "code"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "toCurrencyName", 
                Code = "code"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id =Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = 1.1, 
                BidPrice = -1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.ExchangeRateBidPriceInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Fact]
        public void Validator_NegativeAskPrice_ValidationHasErrors()
        {
            // Arrange
            var fromCurrency = new CurrencyDto()
            {
                Name = "fromCurrencyName", 
                Code = "code"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "toCurrencyName", 
                Code = "code"
            };
            
            var exchangeRate = new ExchangeRateDto()
            {
                Id =Guid.NewGuid(),
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1,
                AskPrice = -1.1, 
                BidPrice = 1.1,
            };
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            // Act
            var validationResult = this.updateExchangeRateValidator.TestValidate(updateExchangeRate);
            
            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.ExchangeRateAskPriceInvalid, validationResult.Errors[0].ErrorMessage);
        }
    }
}