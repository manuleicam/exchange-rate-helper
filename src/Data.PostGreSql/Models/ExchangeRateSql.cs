namespace Data.PostGreSql.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("exchangerates")]
    public class ExchangeRateSql
    {
        [Column("id")]
        public Guid Id { get; set; }
        
        
        [Column("from_currency_name")]
        public string FromCurrencyName { get; set; }
        
        [Column("from_currency_code")]
        public string FromCurrencyCode { get; set; }
        
        [Column("to_currency_name")]
        public string ToCurrencyName { get; set; }
        
        [Column("to_currency_code")]
        public string ToCurrencyCode { get; set; }
        
        [Column("rate")]
        public double Rate { get; set; }

        [Column("bid_price")]
        public double BidPrice { get; set; }

        [Column("ask_price")]
        public double AskPrice { get; set; }
    }
}