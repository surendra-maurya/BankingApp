namespace BankingFactoryPattern.Interfaces
{
    public interface IPaymentMethod
    {
        string PaymentMethodName { get; }

        bool ValidateDetails();
        bool ProcessPayment(decimal amount);
        void DisplayPaymentInfo();
        string GenerateTransactionId();
    }
}
