namespace Application.Services.ExchangeRate.Queries
{
    using FluentValidation;
    using Infrastructure.CrossCutting.Utils;

    public class GetAllExchangeRatesValidator : AbstractValidator<GetAllExchangeRates>
    {
        public GetAllExchangeRatesValidator()
        {
            RuleFor(expression => expression.FromCurrencyCode)
                .Must(code => string.IsNullOrWhiteSpace(code) || code.Length <= Constants.ExchangesRatesValidatorCurrencyCodeMaxLength)
                .WithMessage(ErrorMessages.ExchangesRatesValidatorCurrencyCodeInvalid);

            RuleFor(expression => expression.ToCurrencyCode)
                .Must(code => string.IsNullOrWhiteSpace(code) || code.Length <= Constants.ExchangesRatesValidatorCurrencyCodeMaxLength)
                .WithMessage(ErrorMessages.ExchangesRatesValidatorCurrencyCodeInvalid);
        }
    }
}