namespace exchange_rate_helper.Controllers.ExchangeRate
{
    using Application.DTO.ExchangeRate;
    using Application.Services.Currency.Mappers;
    using Application.Services.ExchangeRate.Commands;
    using Application.Services.ExchangeRate.Queries;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IMediator mediator;

        public ExchangeRateController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id:guid}",  Name = "GetExchangeRate")]
        public async Task<IActionResult> GetExchangeRate(Guid id)
        {
            var getExchangeRate = new GetExchangeRate(id);

            var exchangeRate = await mediator.Send(getExchangeRate);

            return this.Ok(exchangeRate);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllExchangeRates(
            [FromQuery] string? fromCurrencyCode = default,
            [FromQuery] string? toCurrencyCode = default)
        {
            var getAllExchangeRates = new GetAllExchangeRates(fromCurrencyCode, toCurrencyCode);

            var exchangeRates = await mediator.Send(getAllExchangeRates);

            return this.Ok(exchangeRates);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateExchangeRate(
            [FromBody] ExchangeRateDto exchangeRateDto)
        {
            var createExchangeRate = new CreateExchangeRate(
                exchangeRateDto.FromCurrency,
                exchangeRateDto.ToCurrency,
                exchangeRateDto.Rate,
                exchangeRateDto.BidPrice,
                exchangeRateDto.AskPrice);

            var exchangeRateCreated = await mediator.Send(createExchangeRate);

            return this.CreatedAtRoute("GetExchangeRate", new { id = exchangeRateCreated},exchangeRateDto);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateExchangeRate(
            Guid id,
            [FromBody] ExchangeRateDto exchangeRateDto)
        {
            var updateExchangeRate = new UpdateExchangeRate(
                id,
                exchangeRateDto);

            await mediator.Send(updateExchangeRate);

            return this.NoContent();
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteExchangeRate(Guid id)
        {
            var deleteExchangeRate = new DeleteExchangeRate(id);

            await mediator.Send(deleteExchangeRate);

            return this.NoContent();
        }
    }
}