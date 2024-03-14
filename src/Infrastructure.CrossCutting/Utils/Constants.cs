namespace Infrastructure.CrossCutting.Utils
{
    public static class Constants
    {
        /// <summary>
        /// Currency Domain
        /// </summary>
        public const int CurrencyNameMaxLength = 50;
        public const int CurrencyCodeMaxLength = 10;
        
        /// <summary>
        /// GetAllExchangesRatesValidator
        /// </summary>
        public const int ExchangesRatesValidatorCurrencyCodeMaxLength = 10;
        
        /// <summary>
        /// CreateExchangesRatesValidator
        /// </summary>
        public const int CreateExchangesRatesValidatorCurrencyNameMaxLength = 50;
        public const int CreateExchangesRatesValidatorCurrencyCodeMaxLength = 10;
    }
}