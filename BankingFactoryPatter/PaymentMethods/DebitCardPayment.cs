using BankingFactoryPattern.Interfaces;
using BankingFactoryPattern.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingFactoryPattern.PaymentMethods
{
    /// <summary>
    /// Concrete Product - Debit Card Payment Implementation
    /// </summary>
    public class DebitCardPayment : IPaymentMethod
    {
        private readonly PaymentDetails _details;
        private string _transactionId = string.Empty;

        public string PaymentMethodName => "Debit Card Payment";

        public DebitCardPayment(PaymentDetails details)
        {
            _details = details;
        }

        public bool ValidateDetails()
        {
            Console.WriteLine("\n🔍 Validating Debit Card Details...");

            if (string.IsNullOrEmpty(_details.CardNumber) || _details.CardNumber.Length != 16)
            {
                Console.WriteLine("❌ Invalid Card Number! Should be 16 digits.");
                return false;
            }

            if (string.IsNullOrEmpty(_details.CVV) || _details.CVV.Length != 3)
            {
                Console.WriteLine("❌ Invalid CVV!");
                return false;
            }

            Console.WriteLine($"✅ Card Number: {MaskCardNumber(_details.CardNumber)}");
            Console.WriteLine("✅ Debit Card validated successfully!");

            return true;
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("\n💳 Processing Debit Card Payment...");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            Console.WriteLine("🔐 Connecting to Bank Server...");
            Thread.Sleep(500);

            Console.WriteLine("💰 Checking account balance...");
            Thread.Sleep(500);

            Console.WriteLine("🔒 Processing secure transaction...");
            Thread.Sleep(500);

            _transactionId = GenerateTransactionId();

            Console.WriteLine($"\n✅ ₹{amount:N2} debited from your account!");

            return true;
        }

        public void DisplayPaymentInfo()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║          DEBIT CARD PAYMENT RECEIPT            ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine($"║  Card Number  : {MaskCardNumber(_details.CardNumber!),-29}║");
            Console.WriteLine($"║  Card Holder  : {_details.CardHolderName ?? "N/A",-29}║");
            Console.WriteLine($"║  Transaction  : {_transactionId,-29}║");
            Console.WriteLine($"║  Status       : {"SUCCESS",-29}║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        public string GenerateTransactionId()
        {
            return $"DC{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
        }

        private string MaskCardNumber(string cardNumber)
        {
            return $"XXXX-XXXX-XXXX-{cardNumber[^4..]}";
        }
    }
}
