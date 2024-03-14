namespace Application.Services.ExchangeRate.Commands
{

    using Application.Services.Currency.Mappers;
    using Domain.Core.Repositories;
    using Domain.Model.ExchangeRate;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using MediatR;

    public class CreateExchangeRateHandler : IRequestHandler<CreateExchangeRate, Guid>
    {
        private readonly IExchangeRateRepository exchangeRateRepository;

        public CreateExchangeRateHandler(IExchangeRateRepository exchangeRateRepository)
        {
            this.exchangeRateRepository = exchangeRateRepository;
        }
        
        public async Task<Guid> Handle(CreateExchangeRate createExchangeRate, CancellationToken cancellationToken = default)
        {
            var exchangeRates = await
                this.exchangeRateRepository.GetExchangeRateByCurrency(
                    createExchangeRate.FromCurrency.Code,
                    createExchangeRate.ToCurrency.Code);
            
            if (exchangeRates.Any())
            {
                throw new ResourceAlreadyExist(ErrorMessages.ExchangesRateAlreadyExist);
            }
            
            var exchangeRateToCreate = new ExchangeRate(
                Guid.NewGuid(),
                createExchangeRate.FromCurrency.ToModel(),
                createExchangeRate.ToCurrency.ToModel(),
                createExchangeRate.Rate,
                createExchangeRate.BidPrice,
                createExchangeRate.AskPrice);

            var exchangeRateCreated = await this.exchangeRateRepository.InsertExchangeRate(exchangeRateToCreate);

            return exchangeRateCreated.Id;
        }
    }
}