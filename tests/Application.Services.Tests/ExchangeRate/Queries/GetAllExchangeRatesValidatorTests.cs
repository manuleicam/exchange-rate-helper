namespace Application.Services.Tests.ExchangeRate.Queries
{
    using Application.Services.ExchangeRate.Queries;
    using FluentValidation.TestHelper;
    using Infrastructure.CrossCutting.Utils;
    using Xunit;

    public class GetAllExchangeRatesValidatorTests
    {
        private readonly GetAllExchangeRatesValidator getAllExchangeRatesValidator;

        public GetAllExchangeRatesValidatorTests()
        {
            this.getAllExchangeRatesValidator = new GetAllExchangeRatesValidator();
        }

        [Theory]
        [InlineData(" "," ")]
        [InlineData("","")]
        [InlineData(null,null)]
        [InlineData("USD",null)]
        [InlineData(null,"USD")]
        [InlineData("USD","USD")]
        public void Validator_AllValid_DoesNotReturnValidationErrors(string fromCurrencyCode, string toCurrencyCode)
        {
            // Arrange
            var getAllExchangeRates = new GetAllExchangeRates(fromCurrencyCode, toCurrencyCode);
            
            // Act
            var validationResult = getAllExchangeRatesValidator.TestValidate(getAllExchangeRates);

            // Assert
            Assert.NotNull(validationResult);
            Assert.True(validationResult.IsValid);
        }
        
        [Fact]
        public void Validator_FromCurrencyCodeTooBig_ReturnValidationErrors()
        {
            // Arrange
            var fromCurrencyCode = new string('B', Constants.ExchangesRatesValidatorCurrencyCodeMaxLength + 1);
            const string toCurrencyCode = "USD";
            
            var getAllExchangeRates = new GetAllExchangeRates(fromCurrencyCode, toCurrencyCode);
            
            // Act
            var validationResult = getAllExchangeRatesValidator.TestValidate(getAllExchangeRates);

            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.ExchangesRatesValidatorCurrencyCodeInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Fact]
        public void Validator_ToCurrencyCodeTooBig_ReturnValidationErrors()
        {
            // Arrange
            const string fromCurrencyCode = "USD";
            var toCurrencyCode = new string('B', Constants.ExchangesRatesValidatorCurrencyCodeMaxLength + 1);
            
            var getAllExchangeRates = new GetAllExchangeRates(fromCurrencyCode, toCurrencyCode);
            
            // Act
            var validationResult = getAllExchangeRatesValidator.TestValidate(getAllExchangeRates);

            // Assert
            Assert.NotNull(validationResult);
            Assert.False(validationResult.IsValid);
            Assert.Single(validationResult.Errors);
            Assert.Equal(ErrorMessages.ExchangesRatesValidatorCurrencyCodeInvalid, validationResult.Errors[0].ErrorMessage);
        }
        
        [Fact]
        public void Validator_ToCurrencyAndFromCurrencyCodeTooBig_ReturnValidationErrors()
        {
            // Arrange
            var fromCurrencyCode = new string('A', Constants.ExchangesRatesValidatorCurrencyCodeMaxLength + 1);
            var toCurrencyCode = new string('B', Constants.ExchangesRatesValidatorCurrencyCodeMaxLength + 1);
            
            var getAllExchangeRates = new GetAllExchangeRates(fromCurrencyCode, toCurrencyCode);
            
            // Act
            var validationResult = getAllExchangeRatesValidator.TestValidate(getAllExchangeRates);

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Equal(2, validationResult.Errors.Count);
            
            Assert.Equal(ErrorMessages.ExchangesRatesValidatorCurrencyCodeInvalid, validationResult.Errors[0].ErrorMessage);
            Assert.Equal(ErrorMessages.ExchangesRatesValidatorCurrencyCodeInvalid, validationResult.Errors[0].ErrorMessage);
        }
    }
}