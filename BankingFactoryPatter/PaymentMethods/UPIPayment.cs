using BankingFactoryPattern.Interfaces;
using BankingFactoryPattern.Models;

namespace BankingFactoryPattern.PaymentMethods
{
    /// <summary>
    /// Concrete Product - UPI Payment Implementation
    /// </summary>
    public class UPIPayment : IPaymentMethod
    {
        private readonly PaymentDetails _details;
        private string _transactionId = string.Empty;

        public string PaymentMethodName => "UPI Payment";

        public UPIPayment(PaymentDetails details)
        {
            _details = details;
        }

        public bool ValidateDetails()
        {
            Console.WriteLine("\n🔍 Validating UPI Details...");

            if (string.IsNullOrEmpty(_details.UpiId))
            {
                Console.WriteLine("❌ UPI ID is required!");
                return false;
            }

            // Check UPI ID format (example@upi)
            if (!_details.UpiId.Contains("@"))
            {
                Console.WriteLine("❌ Invalid UPI ID format! Should be like: name@upi");
                return false;
            }

            Console.WriteLine($"✅ UPI ID '{_details.UpiId}' is valid!");
            return true;
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("\n💳 Processing UPI Payment...");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            // Simulate payment processing
            Console.WriteLine("📱 Opening UPI App...");
            Thread.Sleep(500);

            Console.WriteLine("🔐 Authenticating with UPI PIN...");
            Thread.Sleep(500);

            Console.WriteLine("💰 Debiting amount from linked bank account...");
            Thread.Sleep(500);

            _transactionId = GenerateTransactionId();

            Console.WriteLine($"\n✅ Payment of ₹{amount:N2} successful!");
            Console.WriteLine($"📧 Confirmation sent to UPI ID: {_details.UpiId}");

            return true;
        }

        public void DisplayPaymentInfo()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║         UPI PAYMENT RECEIPT            ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine($"║  UPI ID      : {_details.UpiId,-23}║");
            Console.WriteLine($"║  Transaction : {_transactionId,-23}║");
            Console.WriteLine($"║  Status      : {"SUCCESS",-23}║");
            Console.WriteLine("╚════════════════════════════════════════╝");
        }

        public string GenerateTransactionId()
        {
            return $"UPI{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
        }
    }
}
