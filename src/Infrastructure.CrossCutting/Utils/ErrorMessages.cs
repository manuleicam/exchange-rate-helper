namespace Infrastructure.CrossCutting.Utils
{
    public static class ErrorMessages
    {
        /// <summary>
        /// Currency Domain
        /// </summary>
        public static string CurrencyNameInvalid = $"Currency Name must be between 1 and {Constants.CurrencyNameMaxLength} chars.";
        public static string CurrencyCodeInvalid = $"Currency Name must be between 1 and {Constants.CurrencyCodeMaxLength} chars.";

        /// <summary>
        /// Exchange Rate Domain
        /// </summary>
        public const string ExchangeRateRateInvalid = "Exchange Rate Rate cannot be less than 0";
        public const string ExchangeRateBidPriceInvalid = "Exchange Rate Bid Price cannot be less than 0";
        public const string ExchangeRateAskPriceInvalid = "Exchange Rate Ask Price cannot be less than 0";
        public const string ExchangeRateSameCurrency = "Exchange Rate From and To Currency cannot be the same";

        /// <summary>
        /// Resource Not Found
        /// </summary>
        public const string ExchangeRateNotFound = "Exchange Rate does not exist";
        
        /// <summary>
        /// CurrentExchangeRate gateway
        /// </summary>
        public const string ExternalCallInvalid = "There was an error calling the external source";
        
        /// <summary>
        /// GetAllExchangesValidator
        /// </summary>
        public static string ExchangesRatesValidatorCurrencyCodeInvalid = $"Currency Code must have less than {Constants.ExchangesRatesValidatorCurrencyCodeMaxLength}.";
        
        /// <summary>
        /// CreateExchangeRate
        /// </summary>
        public static string ExchangesRateAlreadyExist = $"There is already a Exchange Rate created for those currencies";
        
        /// <summary>
        /// UpdateExchangeRate
        /// </summary>
        public static string ExchangesRatesAreNotTheSame = $"The Exchange Rate in with the URL Id is not the same as the Exchange Rate in the request body";
        public static string UpdateExchangeRateIdInvalid = $"The Exchange Rate Id cannot be invalid";
        
        /// <summary>
        /// CreateExchangesRateValidator
        /// </summary>
        public static string CreateExchangesRateCurrencyNameInvalid = $"Currency Name must be between 1 and {Constants.CreateExchangesRatesValidatorCurrencyNameMaxLength} chars.";
        public static string CreateExchangesRateCurrencyCodeInvalid = $"Currency Code must be between 1 and {Constants.CreateExchangesRatesValidatorCurrencyCodeMaxLength} chars.";
    }
}