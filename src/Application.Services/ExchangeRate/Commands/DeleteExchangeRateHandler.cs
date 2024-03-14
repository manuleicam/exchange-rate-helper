namespace Application.Services.ExchangeRate.Commands
{
    using Domain.Core.Repositories;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;
    using MediatR;

    public class DeleteExchangeRateHandler : IRequestHandler<DeleteExchangeRate>
    {
        private readonly IExchangeRateRepository exchangeRateRepository;
        
        public DeleteExchangeRateHandler(IExchangeRateRepository exchangeRateRepository)
        {
            this.exchangeRateRepository = exchangeRateRepository;
        }
        
        public async Task Handle(DeleteExchangeRate deleteExchangeRate, CancellationToken cancellationToken = default)
        {
            var exchangeRate = await this.exchangeRateRepository.GetExchangeRate(deleteExchangeRate.Id);

            if (exchangeRate == null)
            {
                throw new ResourceNotFound(ErrorMessages.ExchangeRateNotFound);
            }
            
            await this.exchangeRateRepository.DeleteExchangeRate(deleteExchangeRate.Id);
        }
    }

}