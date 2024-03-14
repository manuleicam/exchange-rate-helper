namespace Application.Services.ExchangeRate.Commands
{
    using FluentValidation;
    using Infrastructure.CrossCutting.Utils;

    public class UpdateExchangeRateValidator: AbstractValidator<UpdateExchangeRate>
    {
        public UpdateExchangeRateValidator()
        {
            RuleFor(expression => expression.ExchangeRate.Id)
                .NotNull()
                .WithMessage(ErrorMessages.UpdateExchangeRateIdInvalid);
            
            RuleFor(expression => expression.ExchangeRate.FromCurrency.Code)
                .Must(code => !string.IsNullOrWhiteSpace(code) && code.Length <= Constants.CreateExchangesRatesValidatorCurrencyCodeMaxLength)
                .WithMessage(code => ErrorMessages.CreateExchangesRateCurrencyCodeInvalid);
            
            RuleFor(expression => expression.ExchangeRate.FromCurrency.Name)
                .Must(name => !string.IsNullOrWhiteSpace(name) && name.Length <= Constants.CreateExchangesRatesValidatorCurrencyNameMaxLength)
                .WithMessage(ErrorMessages.CreateExchangesRateCurrencyNameInvalid);

            RuleFor(expression => expression.ExchangeRate.ToCurrency.Code)
                .Must(code => !string.IsNullOrWhiteSpace(code) && code.Length <= Constants.CreateExchangesRatesValidatorCurrencyCodeMaxLength)
                .WithMessage(ErrorMessages.CreateExchangesRateCurrencyCodeInvalid);
            
            RuleFor(expression => expression.ExchangeRate.ToCurrency.Name)
                .Must(name => !string.IsNullOrWhiteSpace(name) && name.Length <= Constants.CreateExchangesRatesValidatorCurrencyNameMaxLength)
                .WithMessage(ErrorMessages.CreateExchangesRateCurrencyNameInvalid);

            RuleFor(expression => expression.ExchangeRate.Rate)
                .NotNull()
                .GreaterThanOrEqualTo(default(double))
                .WithMessage(ErrorMessages.ExchangeRateRateInvalid);
            
            RuleFor(expression => expression.ExchangeRate.BidPrice)
                .NotNull()
                .GreaterThanOrEqualTo(default(double))
                .WithMessage(ErrorMessages.ExchangeRateBidPriceInvalid);
            
            RuleFor(expression => expression.ExchangeRate.AskPrice)
                .NotNull()
                .GreaterThanOrEqualTo(default(double))
                .WithMessage(ErrorMessages.ExchangeRateAskPriceInvalid);
        }
    }
}