namespace Application.Services.ExchangeRate.Commands
{
    using Application.DTO.Currency;
    using MediatR;

    public class CreateExchangeRate : IRequest<Guid>
    {
        public CreateExchangeRate(
            CurrencyDto fromCurrency,
            CurrencyDto toCurrency,
            double rate,
            double bidPrice,
            double askPrice)
        {
            this.FromCurrency = fromCurrency;
            this.ToCurrency = toCurrency;
            this.Rate = rate;
            this.BidPrice = bidPrice;
            this.AskPrice = askPrice;
        }
        
        public CurrencyDto FromCurrency { get; set; }

        public CurrencyDto ToCurrency { get; set; }

        public double Rate { get; set; }

        public double BidPrice { get; set; }

        public double AskPrice { get; set; }
    }
}