namespace Application.DTO.ExchangeRate
{

    using System.Data.Common;
    using Application.DTO.Currency;

    public class ExchangeRateDto
    {
        public Guid? Id { get; set; }
        
        public CurrencyDto FromCurrency { get; set; }

        public CurrencyDto ToCurrency { get; set; }

        public double Rate { get; set; }

        public double BidPrice { get; set; }

        public double AskPrice { get; set; }
    }
}