using BankingFactoryPattern.Interfaces;
using BankingFactoryPattern.Models;

namespace BankingFactoryPattern.PaymentMethods
{
    /// <summary>
    /// Concrete Product - Credit Card Payment Implementation
    /// </summary>
    public class CreditCardPayment : IPaymentMethod
    {
        private readonly PaymentDetails _details;
        private string _transactionId = string.Empty;

        public string PaymentMethodName => "Credit Card Payment";

        public CreditCardPayment(PaymentDetails details)
        {
            _details = details;
        }

        public bool ValidateDetails()
        {
            Console.WriteLine("\n🔍 Validating Credit Card Details...");

            if (string.IsNullOrEmpty(_details.CardNumber) || _details.CardNumber.Length != 16)
            {
                Console.WriteLine("❌ Invalid Card Number! Should be 16 digits.");
                return false;
            }

            if (string.IsNullOrEmpty(_details.CVV) || _details.CVV.Length != 3)
            {
                Console.WriteLine("❌ Invalid CVV! Should be 3 digits.");
                return false;
            }

            if (string.IsNullOrEmpty(_details.ExpiryDate))
            {
                Console.WriteLine("❌ Expiry Date is required!");
                return false;
            }

            Console.WriteLine($"✅ Card Number: {MaskCardNumber(_details.CardNumber)}");
            Console.WriteLine($"✅ Card Type: {GetCardType(_details.CardNumber)}");
            Console.WriteLine("✅ Card details validated successfully!");

            return true;
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("\n💳 Processing Credit Card Payment...");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            Console.WriteLine("🔐 Connecting to Payment Gateway...");
            Thread.Sleep(500);

            Console.WriteLine("🔒 Encrypting card details...");
            Thread.Sleep(500);

            Console.WriteLine("📡 Communicating with Card Network...");
            Thread.Sleep(500);

            Console.WriteLine("✓ Authorization received from bank...");
            Thread.Sleep(500);

            _transactionId = GenerateTransactionId();

            Console.WriteLine($"\n✅ Payment of ₹{amount:N2} charged to Credit Card!");
            Console.WriteLine($"💳 Card: {MaskCardNumber(_details.CardNumber!)}");

            return true;
        }

        public void DisplayPaymentInfo()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║         CREDIT CARD PAYMENT RECEIPT            ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine($"║  Card Number  : {MaskCardNumber(_details.CardNumber!),-29}║");
            Console.WriteLine($"║  Card Type    : {GetCardType(_details.CardNumber!),-29}║");
            Console.WriteLine($"║  Card Holder  : {_details.CardHolderName ?? "N/A",-29}║");
            Console.WriteLine($"║  Transaction  : {_transactionId,-29}║");
            Console.WriteLine($"║  Status       : {"SUCCESS",-29}║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        public string GenerateTransactionId()
        {
            return $"CC{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
        }

        private string MaskCardNumber(string cardNumber)
        {
            return $"XXXX-XXXX-XXXX-{cardNumber[^4..]}";
        }

        private string GetCardType(string cardNumber)
        {
            return cardNumber[0] switch
            {
                '4' => "VISA",
                '5' => "MasterCard",
                '3' => "American Express",
                '6' => "Discover",
                _ => "Unknown"
            };
        }
    }
}
