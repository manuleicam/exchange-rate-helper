namespace Application.Services.ExchangeRate.Commands
{
    using MediatR;

    public class DeleteExchangeRate : IRequest
    {
        public DeleteExchangeRate(Guid id)
        {
            this.Id = id;
        }
        
        public Guid Id { get; set; }
    }
}