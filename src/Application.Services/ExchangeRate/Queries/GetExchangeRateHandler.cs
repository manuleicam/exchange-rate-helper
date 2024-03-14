namespace Application.Services.ExchangeRate.Queries
{
    using Application.DTO.ExchangeRate;
    using Application.Services.ExchangeRate.Mappers;
    using Application.Services.Gateways;
    using Domain.Core.Repositories;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using MediatR;

    public class GetExchangeRateHandler : IRequestHandler<GetExchangeRate, ExchangeRateDto>
    {
        private readonly IExchangeRateRepository ExchangeRateRepository;

        public GetExchangeRateHandler(IExchangeRateRepository exchangeRateRepository)
        {
            this.ExchangeRateRepository = exchangeRateRepository;
        }
        
        public async Task<ExchangeRateDto> Handle(GetExchangeRate getExchangeRate, CancellationToken cancellationToken = default)
        {
            var exchangeRate = await this.ExchangeRateRepository.GetExchangeRate(getExchangeRate.Id);

            if (exchangeRate == null)
            {
                throw new ResourceNotFound(ErrorMessages.ExchangeRateNotFound);
            }

            return exchangeRate.ToDto();
        }
    }
}