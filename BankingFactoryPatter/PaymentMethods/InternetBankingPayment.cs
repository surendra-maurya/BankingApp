using BankingFactoryPattern.Interfaces;
using BankingFactoryPattern.Models;

namespace BankingFactoryPattern.PaymentMethods
{

    /// <summary>
    /// Concrete Product - Internet Banking Implementation
    /// </summary>
    public class InternetBankingPayment : IPaymentMethod
    {
        private readonly PaymentDetails _details;
        private string _transactionId = string.Empty;

        public string PaymentMethodName => "Internet Banking (NEFT/RTGS)";

        public InternetBankingPayment(PaymentDetails details)
        {
            _details = details;
        }

        public bool ValidateDetails()
        {
            Console.WriteLine("\n🔍 Validating Internet Banking Details...");

            if (string.IsNullOrEmpty(_details.AccountNumber))
            {
                Console.WriteLine("❌ Account Number is required!");
                return false;
            }

            if (_details.AccountNumber.Length < 9 || _details.AccountNumber.Length > 18)
            {
                Console.WriteLine("❌ Invalid Account Number length!");
                return false;
            }

            if (string.IsNullOrEmpty(_details.IFSCCode) || _details.IFSCCode.Length != 11)
            {
                Console.WriteLine("❌ Invalid IFSC Code! Should be 11 characters.");
                return false;
            }

            Console.WriteLine($"✅ Account Number: {MaskAccountNumber(_details.AccountNumber)}");
            Console.WriteLine($"✅ IFSC Code: {_details.IFSCCode}");
            Console.WriteLine("✅ Bank details validated successfully!");

            return true;
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine("\n🏦 Processing Internet Banking Payment...");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            Console.WriteLine("🔐 Connecting to Banking Server...");
            Thread.Sleep(500);

            Console.WriteLine("📋 Verifying beneficiary account...");
            Thread.Sleep(500);

            Console.WriteLine("🔒 Processing through secure gateway...");
            Thread.Sleep(500);

            Console.WriteLine("💸 Transferring funds via NEFT...");
            Thread.Sleep(500);

            _transactionId = GenerateTransactionId();

            Console.WriteLine($"\n✅ Transfer of ₹{amount:N2} successful!");
            Console.WriteLine($"📧 SMS sent to registered mobile number");

            return true;
        }

        public void DisplayPaymentInfo()
        {
            Console.WriteLine("\n╔════════════════════════════════════════════════╗");
            Console.WriteLine("║       INTERNET BANKING PAYMENT RECEIPT         ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine($"║  Account No.  : {MaskAccountNumber(_details.AccountNumber!),-29}║");
            Console.WriteLine($"║  IFSC Code    : {_details.IFSCCode,-29}║");
            Console.WriteLine($"║  Beneficiary  : {_details.AccountHolderName ?? "N/A",-29}║");
            Console.WriteLine($"║  Transaction  : {_transactionId,-29}║");
            Console.WriteLine($"║  Mode         : {"NEFT",-29}║");
            Console.WriteLine($"║  Status       : {"SUCCESS",-29}║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        public string GenerateTransactionId()
        {
            return $"NEFT{DateTime.Now:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
        }

        private string MaskAccountNumber(string accountNumber)
        {
            if (accountNumber.Length <= 4) return accountNumber;
            return new string('X', accountNumber.Length - 4) + accountNumber[^4..];
        }
    }
}
