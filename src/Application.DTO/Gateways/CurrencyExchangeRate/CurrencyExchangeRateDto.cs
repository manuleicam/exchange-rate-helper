namespace Application.DTO.Gateways.CurrencyExchangeRate
{

    using Newtonsoft.Json;

    public class CurrencyExchangeRateDto
    {
        [JsonProperty("Realtime Currency Exchange Rate")]
        public MetaData MetaData { get; set; }
    }

}