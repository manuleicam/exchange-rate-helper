namespace Application.Services.ExchangeRate.Queries
{
    using Application.DTO.ExchangeRate;
    using Application.Services.ExchangeRate.Mappers;
    using Application.Services.Gateways;
    using Domain.Core.Repositories;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class GetAllExchangeRatesHandler : IRequestHandler<GetAllExchangeRates, IEnumerable<ExchangeRateDto>>
    {
        private readonly IExchangeRateRepository ExchangeRateRepository;
        private readonly ICurrencyExchangeRateGateway CurrencyExchangeRateGateway;
        private readonly ILogger<GetAllExchangeRatesHandler> logger;

        public GetAllExchangeRatesHandler(
            IExchangeRateRepository exchangeRateRepository,
            ICurrencyExchangeRateGateway currencyExchangeRateGateway,
            ILogger<GetAllExchangeRatesHandler> logger)
        {
            this.logger = logger;
            this.ExchangeRateRepository = exchangeRateRepository;
            this.CurrencyExchangeRateGateway = currencyExchangeRateGateway;
        }
        
        public async Task<IEnumerable<ExchangeRateDto>> Handle(GetAllExchangeRates getAllExchangeRates, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(getAllExchangeRates.FromCurrencyCode) &&
                string.IsNullOrWhiteSpace(getAllExchangeRates.ToCurrencyCode))
            {
                var exchangeRates = await this.ExchangeRateRepository.GetAll();
                
                return exchangeRates.Select(er => er.ToDto());
            }

            return await GetExchangeRateByCurrencies(
                getAllExchangeRates.FromCurrencyCode,
                getAllExchangeRates.ToCurrencyCode);
        }

        private async Task<IEnumerable<ExchangeRateDto>> GetExchangeRateByCurrencies(
            string? fromCurrencyCode = default,
            string? toCurrencyCode = default)
        {
            var exchangeRates = (await this.ExchangeRateRepository.GetExchangeRateByCurrency(
                fromCurrencyCode,
                toCurrencyCode)).ToList();

            if (!string.IsNullOrWhiteSpace(fromCurrencyCode) &&
                !string.IsNullOrWhiteSpace(toCurrencyCode) &&
                !exchangeRates.Any())
            {
                try
                {
                    var exchangeRate = await this.CurrencyExchangeRateGateway.GetExchangeRate(fromCurrencyCode, toCurrencyCode);
                    exchangeRates.Add(exchangeRate);
                }
                catch (Exception exception)
                {
                    this.logger.LogError(
                        null,
                        $"Alphavantage external call Failed: {fromCurrencyCode} and ToCurrencyCode: {toCurrencyCode}",
                        new
                        {
                            FromCurrencyCode = fromCurrencyCode,
                            ToCurrencyCode = toCurrencyCode,
                        });
                    throw new ExternalCallException(ErrorMessages.ExternalCallInvalid);
                }
            }

            return exchangeRates.Select(er => er.ToDto());
        }
    }
}