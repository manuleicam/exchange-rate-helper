namespace Application.Services.Tests.ExchangeRate.Commands
{

    using Application.DTO.Currency;
    using Application.Services.ExchangeRate.Commands;
    using Domain.Core.Repositories;
    using Domain.Model.Currency;
    using Domain.Model.ExchangeRate;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using NSubstitute;
    using Xunit;

    public class CreateExchangeRateHandlerTests
    {
        private readonly IExchangeRateRepository exchangeRateRepository;
        private readonly CreateExchangeRateHandler createExchangeRateHandler;
        
        public CreateExchangeRateHandlerTests()
        {
            this.exchangeRateRepository = Substitute.For<IExchangeRateRepository>();
            this.createExchangeRateHandler = new CreateExchangeRateHandler(exchangeRateRepository);
        }

        [Fact]
        public async Task Handle_ExchangeRateIsNew_ReturnExchangeRateGuid()
        {
            // Arrange
            var exchangeRates = new List<ExchangeRate>();
            
            var createExchangeRate = BuildCreateExchangeRate();
            
            this.exchangeRateRepository.GetExchangeRateByCurrency(Arg.Any<string>(),Arg.Any<string>()).Returns(exchangeRates);
            this.exchangeRateRepository.InsertExchangeRate(Arg.Any<ExchangeRate>()).Returns(this.BuildExchangeRate());
            
            // Act
            var createdID = await this.createExchangeRateHandler.Handle(createExchangeRate);

            // Assert
            Assert.NotNull(createdID);
            Assert.IsType<Guid>(createdID);

            await exchangeRateRepository.Received(1).GetExchangeRateByCurrency(Arg.Any<string>(),Arg.Any<string>());
            await exchangeRateRepository.Received(1).InsertExchangeRate(Arg.Any<ExchangeRate>());
        }
        
        [Fact]
        public async Task Handle_ExchangeRateIsNotNew_ThrowsResourceAlreadyExistException()
        {
            // Arrange
            var exchangeRates = new List<ExchangeRate>()
            {
                this.BuildExchangeRate(),
            };
            
            var createExchangeRate = BuildCreateExchangeRate();
            
            this.exchangeRateRepository.GetExchangeRateByCurrency(Arg.Any<string>(),Arg.Any<string>()).Returns(exchangeRates);
            
            // Act
            Func<Task> act = async () => await this.createExchangeRateHandler.Handle(createExchangeRate);

            // Assert
            Assert.NotNull(act);
            var exception = await Assert.ThrowsAsync<ResourceAlreadyExist>(act);
            Assert.Equal(ErrorMessages.ExchangesRateAlreadyExist, exception.Message);

            await exchangeRateRepository.Received(1).GetExchangeRateByCurrency(Arg.Any<string>(),Arg.Any<string>());
            await exchangeRateRepository.DidNotReceive().InsertExchangeRate(Arg.Any<ExchangeRate>());
        }
        
        private ExchangeRate BuildExchangeRate()
        {
            var fromCurrency = new Currency("United States Dollar", "USD");
            var toCurrency = new Currency("Euro", "EUR");
            
            return new ExchangeRate(Guid.NewGuid(), fromCurrency, toCurrency, 1.1, 1.1, 1.1);
        }
        
        private CreateExchangeRate BuildCreateExchangeRate()
        {
            var fromCurrency = new CurrencyDto()
            {
                Name = "Euro", 
                Code = "EUR"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "Japan", 
                Code = "JPY"
            };
            
            return new CreateExchangeRate(fromCurrency, toCurrency, 1.1, 1.1, 1.1);
        }
    }
}