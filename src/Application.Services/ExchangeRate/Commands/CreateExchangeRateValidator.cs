namespace Application.Services.ExchangeRate.Commands
{
    using FluentValidation;
    using Infrastructure.CrossCutting.Utils;

    public class CreateExchangeRateValidator : AbstractValidator<CreateExchangeRate>
    {
        public CreateExchangeRateValidator()
        {
            RuleFor(expression => expression.FromCurrency.Code)
                .Must(code => !string.IsNullOrWhiteSpace(code) && code.Length <= Constants.CreateExchangesRatesValidatorCurrencyCodeMaxLength)
                .WithMessage(code => ErrorMessages.CreateExchangesRateCurrencyCodeInvalid);
            
            RuleFor(expression => expression.FromCurrency.Name)
                .Must(name => !string.IsNullOrWhiteSpace(name) && name.Length <= Constants.CreateExchangesRatesValidatorCurrencyNameMaxLength)
                .WithMessage(ErrorMessages.CreateExchangesRateCurrencyNameInvalid);

            RuleFor(expression => expression.ToCurrency.Code)
                .Must(code => !string.IsNullOrWhiteSpace(code) && code.Length <= Constants.CreateExchangesRatesValidatorCurrencyCodeMaxLength)
                .WithMessage(ErrorMessages.CreateExchangesRateCurrencyCodeInvalid);
            
            RuleFor(expression => expression.ToCurrency.Name)
                .Must(name => !string.IsNullOrWhiteSpace(name) && name.Length <= Constants.CreateExchangesRatesValidatorCurrencyNameMaxLength)
                .WithMessage(ErrorMessages.CreateExchangesRateCurrencyNameInvalid);
            
            RuleFor(expression => expression.Rate)
                .GreaterThanOrEqualTo(default(double))
                .WithMessage(ErrorMessages.ExchangeRateRateInvalid);
            
            RuleFor(expression => expression.BidPrice)
                .GreaterThanOrEqualTo(default(double))
                .WithMessage(ErrorMessages.ExchangeRateBidPriceInvalid);
            
            RuleFor(expression => expression.AskPrice)
                .GreaterThanOrEqualTo(default(double))
                .WithMessage(ErrorMessages.ExchangeRateAskPriceInvalid);
        }
    }
}