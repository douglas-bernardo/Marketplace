using Marketplace.Framework;

namespace Marketplace.Domain
{
    public class Money : Value<Money>
    {
        public static Money FromDecimal(
            decimal amount,
            string currency,
            ICurrencyLookup currencyLookup
            ) => new Money(amount, currency, currencyLookup);

        public static Money FromString(
            string amount,
            string currency,
            ICurrencyLookup currencyLookup
            ) =>
            new Money(decimal.Parse(amount), currency, currencyLookup);

        protected Money(
            decimal amount,
            string currencyCode,
            ICurrencyLookup currencyLookup
            )
        {
            if (string.IsNullOrEmpty(currencyCode))
                throw new ArgumentNullException(
                    nameof(currencyCode),
                    "Currency code must be specified");

            var currency = currencyLookup.FindCurrency(currencyCode);
            if (!currency.InUse) throw new ArgumentException($"Currency {currencyCode} is not valid");

            if (decimal.Round(amount, currency.DecimalPlaces) != amount)
                throw new ArgumentOutOfRangeException(nameof(amount),
                $"Amount in {currencyCode} cannot have more than {currency.DecimalPlaces} decimals");

            Amount = amount;
            Currency = currency;
        }

        protected Money(decimal amount, CurrencyDetails currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }
        public CurrencyDetails Currency { get; }

        public Money Add(Money summand)
        {
            if (Currency != summand.Currency)
                throw new CurrencyMismatchException(
                "Cannot sum amounts with different currencies");
            return new Money(Amount + summand.Amount, Currency);
        }
        public Money Subtract(Money subtrahend)
        {
            if (Currency != subtrahend.Currency)
                throw new CurrencyMismatchException(
                "Cannot subtract amounts with different currencies");
            return new Money(Amount - subtrahend.Amount, Currency);
        }

        public static Money operator +(Money summand1, Money summand2)
            => summand1.Add(summand2);

        public static Money operator -(Money minuend, Money subtrahend)
            => minuend.Subtract(subtrahend);

        public override string ToString() => $"{Currency.CurrencyCode} {Amount}";
    }

    // Insight: The CurrencyMismatchException class is a custom exception that is thrown
    // when there is an attempt to perform arithmetic operations (addition or subtraction)
    // on Money objects with different currency codes. It inherits from the base Exception class
    // and takes a message parameter in its constructor to provide details about the exception.
    // The current location of the CurrencyMismatchException class is within the Marketplace.Domain namespace,
    // which is appropriate since it is closely related to the Money class and its operations.
    // However, if you have a dedicated folder or namespace for exceptions in your project structure,
    // it might be more organized to place it there.
    // For example, you could create a namespace like Marketplace.Domain.Exceptions and
    // move the CurrencyMismatchException class into that namespace.
    // This would help keep your codebase organized and make it easier to locate exception classes in the future.
    public class CurrencyMismatchException : Exception
    {
        public CurrencyMismatchException(string message) :
        base(message)
        {
        }
    }
}
