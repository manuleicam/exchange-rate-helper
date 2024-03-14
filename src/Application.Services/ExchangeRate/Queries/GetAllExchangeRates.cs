namespace Application.Services.ExchangeRate.Queries
{
    using Application.DTO.ExchangeRate;
    using MediatR;

    public class GetAllExchangeRates : IRequest<IEnumerable<ExchangeRateDto>>
    {
        public GetAllExchangeRates(string fromCurrencyCode, string toCurrencyCode)
        {
            this.FromCurrencyCode = fromCurrencyCode;
            this.ToCurrencyCode = toCurrencyCode;
        }
        
        public string? FromCurrencyCode { get; set; }
        
        public string? ToCurrencyCode { get; set; }
    }
}