namespace Domain.Model.ExchangeRate
{
    using Domain.Model.Currency;
    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;

    public class ExchangeRate
    {
        private double rate;
        private double bidPrice;
        private double askPrice;

        public ExchangeRate(
            Guid id,
            Currency fromCurrency,
            Currency toCurrency,
            double rate,
            double bidPrice,
            double askPrice)
        {
            if (fromCurrency.Name == toCurrency.Name && fromCurrency.Code == toCurrency.Code)
            {
                throw new DomainModelException(ErrorMessages.ExchangeRateSameCurrency);
            }
            
            this.Id = id;
            this.FromCurrency = new Currency(fromCurrency.Name, fromCurrency.Code);
            this.ToCurrency = new Currency(toCurrency.Name, toCurrency.Code);

            this.Rate = rate;
            this.BidPrice = bidPrice;
            this.AskPrice = askPrice;
        }
        
        public Guid Id { get; private set; }
        public Currency FromCurrency { get; private set; }
        public Currency ToCurrency { get; private set; }

        public double Rate
        {
            get => this.rate;
            private set => this.SetRate(value);
        }
        
        public double BidPrice
        {
            get => this.bidPrice;
            private set => this.SetBidPrice(value);
        }
        
        public double AskPrice
        {
            get => this.askPrice;
            private set => this.SetAskPrice(value);
        }

        private void SetRate(double rate)
        {
            this.rate = rate >= default(double) ? rate
                : throw new DomainModelException(ErrorMessages.ExchangeRateRateInvalid);
        }
        
        private void SetBidPrice(double bidPrice)
        {
            this.bidPrice = bidPrice >= default(double) ? bidPrice
                : throw new DomainModelException(ErrorMessages.ExchangeRateBidPriceInvalid);
        }
        
        private void SetAskPrice(double askPrice)
        {
            this.askPrice = askPrice >= default(double) ? askPrice
                : throw new DomainModelException(ErrorMessages.ExchangeRateAskPriceInvalid);
        }

        public bool AreEqualCurrencies(ExchangeRate exchangeRate)
        {
            return this.Id == exchangeRate.Id &&
                   this.FromCurrency.Code == exchangeRate.FromCurrency.Code &&
                   this.FromCurrency.Name == exchangeRate.FromCurrency.Name &&
                   this.ToCurrency.Code == exchangeRate.ToCurrency.Code &&
                   this.ToCurrency.Name == exchangeRate.ToCurrency.Name;
        }
    }
}