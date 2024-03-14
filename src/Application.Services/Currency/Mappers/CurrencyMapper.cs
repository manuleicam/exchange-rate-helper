namespace Application.Services.Currency.Mappers
{

    using Application.DTO.Currency;
    using Domain.Model.Currency;

    public static class CurrencyMapper
    {
        public static Currency ToModel(this CurrencyDto currency)
        {
            if (currency == null)
            {
                return null;
            }
            
            return new Currency(currency.Name, currency.Code);
        }
        
        public static CurrencyDto ToDto(this Currency currency)
        {
            if (currency == null)
            {
                return null;
            }
            
            return new CurrencyDto()
            {
                Name = currency.Name,
                Code = currency.Code,
            };
        }
    }
}