namespace Domain.Model.Currency
{

    using Infrastructure.CrossCutting.Exceptions;
    using Infrastructure.CrossCutting.Utils;

    public class Currency
    {
        private string name;
        private string code;

        public Currency(string name, string code)
        {
            this.Name = name;
            this.Code = code;
        }

        public string Name
        {
            get => this.name;
            private set => this.SetName(value);
        }
        
        public string Code
        {
            get => this.code;
            private set => this.SetCode(value);
        }

        private void SetName(string name)
        {
            this.name = !string.IsNullOrWhiteSpace(name) && name.Length <= Constants.CurrencyNameMaxLength ? name 
                : throw new DomainModelException(ErrorMessages.CurrencyNameInvalid);
        }
        
        private void SetCode(string code)
        {
            this.code = !string.IsNullOrWhiteSpace(code) && code.Length <= Constants.CurrencyCodeMaxLength ? code 
                : throw new DomainModelException(ErrorMessages.CurrencyCodeInvalid);
        }
    }
}