namespace Application.Services.Tests.ExchangeRate.Commands
{
    using Application.Services.ExchangeRate.Commands;
    using Application.Services.ExchangeRate.Queries;
    using Domain.Core.Repositories;
    using Domain.Model.Currency;
    using Domain.Model.ExchangeRate;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using NSubstitute;
    using Xunit;

    public class DeleteExchangeRateHandlerTests
    {
        private readonly IExchangeRateRepository exchangeRateRepository;
        private readonly DeleteExchangeRateHandler deleteExchangeRateHandler;
        
        public DeleteExchangeRateHandlerTests()
        {
            this.exchangeRateRepository = Substitute.For<IExchangeRateRepository>();
            this.deleteExchangeRateHandler = new DeleteExchangeRateHandler(exchangeRateRepository);
        }
        
        [Fact]
        public async Task Handle_ExchangeRateExists_DeleteResource()
        {
            // Arrange
            var exchangeRate = this.BuildExchangeRate();
            var deleteExchangeRate = new DeleteExchangeRate(exchangeRate.Id);
            
            this.exchangeRateRepository.GetExchangeRate(Arg.Any<Guid>()).Returns(exchangeRate);
            
            // Act
            await this.deleteExchangeRateHandler.Handle(deleteExchangeRate);

            // Assert
            await exchangeRateRepository.Received(1).GetExchangeRate(Arg.Any<Guid>());
            await exchangeRateRepository.Received(1).DeleteExchangeRate(Arg.Any<Guid>());
        }
        
        [Fact]
        public async Task Handle_ExchangeRateDoesNotExists_ThrowsResourceNotFoundException()
        {
            // Arrange
            var exchangeRate = this.BuildExchangeRate();
            var deleteExchangeRate = new DeleteExchangeRate(exchangeRate.Id);
            
            this.exchangeRateRepository.GetExchangeRate(Arg.Any<Guid>()).Returns((ExchangeRate)null);
            
            // Act
            Func<Task> act = async () => await this.deleteExchangeRateHandler.Handle(deleteExchangeRate);

            // Assert
            var exception = await Assert.ThrowsAsync<ResourceNotFound>(act);
            Assert.Equal(ErrorMessages.ExchangeRateNotFound, exception.Message);
            
            await exchangeRateRepository.Received(1).GetExchangeRate(Arg.Any<Guid>());
            await exchangeRateRepository.DidNotReceive().DeleteExchangeRate(Arg.Any<Guid>());
        }
        
        private ExchangeRate BuildExchangeRate()
        {
            var fromCurrency = new Currency("United States Dollar", "USD");
            var toCurrency = new Currency("Euro", "EUR");
            
            return new ExchangeRate(Guid.NewGuid(), fromCurrency, toCurrency, 1.1, 1.1, 1.1);
        }
    }
}