namespace Application.Services.ExchangeRate.Commands
{

    using Application.Services.ExchangeRate.Mappers;
    using Domain.Core.Repositories;
    using Domain.Model.ExchangeRate;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using MediatR;

    public class UpdateExchangeRateHandler : IRequestHandler<UpdateExchangeRate>
    {
        private readonly IExchangeRateRepository exchangeRateRepository;
        
        public UpdateExchangeRateHandler(IExchangeRateRepository exchangeRateRepository)
        {
            this.exchangeRateRepository = exchangeRateRepository;
        }
        
        public async Task Handle(UpdateExchangeRate updateExchangeRate, CancellationToken cancellationToken = default)
        {
            var exchangeRate = await this.exchangeRateRepository.GetExchangeRate(updateExchangeRate.Id);
            var newExchangeRate = updateExchangeRate.ExchangeRate.ToModel();

            if (exchangeRate == null)
            {
                throw new ResourceNotFound(ErrorMessages.ExchangeRateNotFound);
            }
            
            if (!exchangeRate.AreEqualCurrencies(newExchangeRate))
            {
                throw new NotSameResourceException(ErrorMessages.ExchangesRatesAreNotTheSame);
            }
            
            await this.exchangeRateRepository.UpdateExchangeRate(updateExchangeRate.Id , newExchangeRate);
        }
    }
}