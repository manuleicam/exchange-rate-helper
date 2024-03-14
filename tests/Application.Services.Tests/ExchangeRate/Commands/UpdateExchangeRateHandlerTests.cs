namespace Application.Services.Tests.ExchangeRate.Commands
{

    using Application.DTO.Currency;
    using Application.DTO.ExchangeRate;
    using Application.Services.ExchangeRate.Commands;
    using Application.Services.ExchangeRate.Mappers;
    using Domain.Core.Repositories;
    using Domain.Model.ExchangeRate;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using NSubstitute;
    using Xunit;

    public class UpdateExchangeRateHandlerTests
    {
        private readonly IExchangeRateRepository exchangeRateRepository;
        private readonly UpdateExchangeRateHandler updateExchangeRateHandler;
        
        public UpdateExchangeRateHandlerTests()
        {
            this.exchangeRateRepository = Substitute.For<IExchangeRateRepository>();
            this.updateExchangeRateHandler = new UpdateExchangeRateHandler(exchangeRateRepository);
        }
        
        [Fact]
        public async Task Handle_ExchangeRateValid_UpdateExchangeRate()
        {
            // Arrange
            var exchangeRate = this.BuildExchangeRate();
            
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            this.exchangeRateRepository.GetExchangeRate(Arg.Any<Guid>()).Returns(exchangeRate.ToModel());
            
            // Act
            await this.updateExchangeRateHandler.Handle(updateExchangeRate);

            // Assert
            await exchangeRateRepository.Received(1).GetExchangeRate(Arg.Any<Guid>());
            await exchangeRateRepository.Received(1).UpdateExchangeRate(Arg.Any<Guid>(), Arg.Any<ExchangeRate>());
        }
        
        [Fact]
        public async Task Handle_ExchangeRateDoesNotExists_ThrowsResourceNotFoundException()
        {
            // Arrange
            var exchangeRate = this.BuildExchangeRate();
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRate);
            
            this.exchangeRateRepository.GetExchangeRate(Arg.Any<Guid>()).Returns((ExchangeRate)null);
            
            // Act
            Func<Task> act = async () => await this.updateExchangeRateHandler.Handle(updateExchangeRate);

            // Assert
            var exception = await Assert.ThrowsAsync<ResourceNotFound>(act);
            Assert.Equal(ErrorMessages.ExchangeRateNotFound, exception.Message);
            
            await exchangeRateRepository.Received(1).GetExchangeRate(Arg.Any<Guid>());
            await exchangeRateRepository.DidNotReceive().UpdateExchangeRate(Arg.Any<Guid>(), Arg.Any<ExchangeRate>());
        }
        
        [Fact]
        public async Task Handle_ExchangeRateAreNotEqual_ThrowsNotSameResourceException()
        {
            // Arrange
            var exchangeRate = this.BuildExchangeRate();
            
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
            var exchangeRateToUpdate = new ExchangeRateDto()
            {
                Id = exchangeRate.Id, 
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1, 
                AskPrice = 1.1,
                BidPrice = 1.1
            };
            
            var updateExchangeRate = new UpdateExchangeRate((Guid)exchangeRate.Id, exchangeRateToUpdate);
            
            this.exchangeRateRepository.GetExchangeRate(Arg.Any<Guid>()).Returns(exchangeRate.ToModel());
            
            // Act
            Func<Task> act = async () => await this.updateExchangeRateHandler.Handle(updateExchangeRate);

            // Assert
            var exception = await Assert.ThrowsAsync<NotSameResourceException>(act);
            Assert.Equal(ErrorMessages.ExchangesRatesAreNotTheSame, exception.Message);
            
            await exchangeRateRepository.Received(1).GetExchangeRate(Arg.Any<Guid>());
            await exchangeRateRepository.DidNotReceive().UpdateExchangeRate(Arg.Any<Guid>(), Arg.Any<ExchangeRate>());
        }
        
        private ExchangeRateDto BuildExchangeRate()
        {
            var fromCurrency = new CurrencyDto()
            {
                Name = "United States", 
                Code = "USD"
            };
            var toCurrency = new CurrencyDto()
            {
                Name = "Japan", 
                Code = "JPY"
            };
            
            return new ExchangeRateDto()
            {
                Id = Guid.NewGuid(), 
                FromCurrency = fromCurrency, 
                ToCurrency = toCurrency, 
                Rate = 1.1, 
                AskPrice = 1.1,
                BidPrice = 1.1
            };
        }
    }
}