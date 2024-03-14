namespace Application.Services.Tests.ExchangeRate.Queries
{

    using Application.DTO.ExchangeRate;
    using Application.Services.ExchangeRate.Queries;
    using Domain.Core.Repositories;
    using Domain.Model.Currency;
    using Domain.Model.ExchangeRate;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using NSubstitute;
    using Xunit;

    public class GetExchangeRateHandlerTests
    {
        private readonly IExchangeRateRepository exchangeRateRepository;
        
        public GetExchangeRateHandlerTests()
        {
            this.exchangeRateRepository = Substitute.For<IExchangeRateRepository>();
        }

        [Fact]
        public async Task Handle_ExchangeRateExists_ReturnsExchangeRate()
        {
            // Arrange
            var exchangeRate = this.BuildExchangeRate();
            var getExchangeRate = new GetExchangeRate(exchangeRate.Id);
            
            this.exchangeRateRepository.GetExchangeRate(Arg.Any<Guid>()).Returns(exchangeRate);

            var getExchangeRateHandler = new GetExchangeRateHandler(exchangeRateRepository);
            
            // Act
            var result = await getExchangeRateHandler.Handle(getExchangeRate);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ExchangeRateDto>(result);

            await exchangeRateRepository.Received(1).GetExchangeRate(Arg.Any<Guid>());
        }
        
        [Fact]
        public async Task Handle_ExchangeRateDoesNotExists_ThrowsResourceNotFoundException()
        {
            // Arrange
            var exchangeRate = this.BuildExchangeRate();
            var getExchangeRate = new GetExchangeRate(exchangeRate.Id);
            
            this.exchangeRateRepository.GetExchangeRate(Arg.Any<Guid>()).Returns((ExchangeRate)null);

            var getExchangeRateHandler = new GetExchangeRateHandler(exchangeRateRepository);
            
            // Act
            Func<Task> act = async () => await getExchangeRateHandler.Handle(getExchangeRate);

            // Assert
            var exception = await Assert.ThrowsAsync<ResourceNotFound>(act);
            Assert.Equal(ErrorMessages.ExchangeRateNotFound, exception.Message);
            
            await exchangeRateRepository.Received(1).GetExchangeRate(Arg.Any<Guid>());
        }

        private ExchangeRate BuildExchangeRate()
        {
            var fromCurrency = new Currency("United States Dollar", "USD");
            var toCurrency = new Currency("Euro", "EUR");
            
            return new ExchangeRate(Guid.NewGuid(), fromCurrency, toCurrency, 1.1, 1.1, 1.1);
        }
    }
}