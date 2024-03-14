namespace Application.Services.Tests.ExchangeRate.Queries
{
    using Application.Services.ExchangeRate.Queries;
    using Application.Services.Gateways;
    using Castle.Core.Logging;
    using Domain.Core.Repositories;
    using Domain.Model.Currency;
    using Domain.Model.ExchangeRate;
    using Microsoft.Extensions.Logging;
    using NSubstitute;
    using Xunit;

    public class GetAllExchangeRatesHandlerTests
    {
        private readonly IExchangeRateRepository exchangeRateRepository;
        private readonly ICurrencyExchangeRateGateway currencyExchangeRateGateway;
        private readonly ILogger<GetAllExchangeRatesHandler> logger;
        private readonly GetAllExchangeRatesHandler getAllExchangeRatesHandler;

        public GetAllExchangeRatesHandlerTests()
        {
            this.exchangeRateRepository = Substitute.For<IExchangeRateRepository>();
            this.currencyExchangeRateGateway = Substitute.For<ICurrencyExchangeRateGateway>();
            this.logger = Substitute.For<ILogger<GetAllExchangeRatesHandler>>();
            this.getAllExchangeRatesHandler = new GetAllExchangeRatesHandler(this.exchangeRateRepository, this.currencyExchangeRateGateway, this.logger);
        }

        [Theory]
        [InlineData(" "," ")]
        [InlineData("","")]
        [InlineData(null,null)]
        public async Task Handle_BothArgumentsAreEmptyOrNull_GoesOnlyToDatabase(string fromCurrencyCode, string toCurrencyCode)
        {
            // Arrange
            var exchangeRates = new List<ExchangeRate>
            {
                BuildExchangeRate(),
            };
            
            var getExchangeRate = new GetAllExchangeRates(fromCurrencyCode, toCurrencyCode);
            this.exchangeRateRepository.GetAll().Returns(exchangeRates);
            
            // Act
            var result = await this.getAllExchangeRatesHandler.Handle(getExchangeRate);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            
            await this.exchangeRateRepository.Received(1).GetAll();
            await this.exchangeRateRepository.DidNotReceive().GetExchangeRateByCurrency(Arg.Any<string>(), Arg.Any<string>());
            await this.exchangeRateRepository.DidNotReceive().GetExchangeRateByCurrency(Arg.Any<string>());
            await this.exchangeRateRepository.DidNotReceive().GetExchangeRateByCurrency();
            await this.currencyExchangeRateGateway.DidNotReceive().GetExchangeRate(Arg.Any<string>(), Arg.Any<string>());
        }
        
        [Theory]
        [InlineData("USD",null)]
        [InlineData("USD","")]
        [InlineData(null,"USD")]
        [InlineData("","USD")]
        [InlineData("Eur","USD")]
        public async Task Handle_OneArgumentIsEmptyOrNull_GoesOnlyToDatabase(string fromCurrencyCode, string toCurrencyCode)
        {
            // Arrange
            var exchangeRates = new List<ExchangeRate>
            {
                BuildExchangeRate(),
            };
            
            var getExchangeRate = new GetAllExchangeRates(fromCurrencyCode, toCurrencyCode);
            this.exchangeRateRepository.GetExchangeRateByCurrency().ReturnsForAnyArgs(exchangeRates);
            
            // Act
            var result = await this.getAllExchangeRatesHandler.Handle(getExchangeRate);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            
            await this.exchangeRateRepository.DidNotReceive().GetAll();
            await this.exchangeRateRepository.Received(1).GetExchangeRateByCurrency(Arg.Any<string>(), Arg.Any<string>());

            if (toCurrencyCode == null)
            {
                await this.exchangeRateRepository.Received(1).GetExchangeRateByCurrency(Arg.Any<string>());
            }
            else
            {
                await this.exchangeRateRepository.DidNotReceive().GetExchangeRateByCurrency(Arg.Any<string>());
            }
            
            await this.exchangeRateRepository.DidNotReceive().GetExchangeRateByCurrency();
            await this.currencyExchangeRateGateway.DidNotReceive().GetExchangeRate(Arg.Any<string>(), Arg.Any<string>());
        }
        
        [Fact]
        public async Task Handle_BothArgumentsAreNotEmptyOrNullAndBdReturnsEmpty_GoesOnlyToDatabase()
        {
            // Arrange
            var exchangeRatesBd = new List<ExchangeRate>();
            const string fromCurrencyCode = "USD";
            const string toCurrencyCode = "EUR";

            var exchangeRateGateway = BuildExchangeRate();
            
            var getExchangeRate = new GetAllExchangeRates(fromCurrencyCode, toCurrencyCode);
            this.exchangeRateRepository.GetAll().Returns(exchangeRatesBd);
            this.currencyExchangeRateGateway.GetExchangeRate(Arg.Any<string>(), Arg.Any<string>()).Returns(exchangeRateGateway);
            
            // Act
            var result = await this.getAllExchangeRatesHandler.Handle(getExchangeRate);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            
            await this.exchangeRateRepository.DidNotReceive().GetAll();
            await this.exchangeRateRepository.Received(1).GetExchangeRateByCurrency(Arg.Any<string>(), Arg.Any<string>());
            await this.exchangeRateRepository.DidNotReceive().GetExchangeRateByCurrency(Arg.Any<string>());
            await this.exchangeRateRepository.DidNotReceive().GetExchangeRateByCurrency();
            await this.currencyExchangeRateGateway.Received(1).GetExchangeRate(Arg.Any<string>(), Arg.Any<string>());
        }
        
        private ExchangeRate BuildExchangeRate()
        {
            var fromCurrency = new Currency("United States Dollar", "USD");
            var toCurrency = new Currency("Euro", "EUR");
            
            return new ExchangeRate(Guid.NewGuid(), fromCurrency, toCurrency, 1.1, 1.1, 1.1);
        }
    }
}