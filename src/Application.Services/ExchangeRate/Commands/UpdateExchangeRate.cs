namespace Application.Services.ExchangeRate.Commands
{
    using Application.DTO.ExchangeRate;
    using MediatR;

    public class UpdateExchangeRate : IRequest
    {
        public UpdateExchangeRate(
            Guid id,
            ExchangeRateDto exchangeRate)
        {
            this.Id = id;
            this.ExchangeRate = exchangeRate;
        }
        
        public Guid Id { get; set; }
        
        public ExchangeRateDto ExchangeRate { get; set; }
    }
}