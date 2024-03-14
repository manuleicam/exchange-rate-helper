namespace Application.Services.ExchangeRate.Queries
{
    using Application.DTO.ExchangeRate;
    using MediatR;

    public class GetExchangeRate(Guid id) : IRequest<ExchangeRateDto>
    {
        public Guid Id = id;
    }
}