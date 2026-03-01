// Program.cs
using BankingFactoryPattern.Enums;
using BankingFactoryPattern.Factory;
using BankingFactoryPattern.Interfaces;
using BankingFactoryPattern.Models;

namespace BankingFactoryPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DisplayWelcomeBanner();

            bool continueApp = true;

            while (continueApp)
            {
                try
                {
                    // Step 1: Show payment options
                    PaymentFactory.DisplayAvailablePaymentMethods();

                    // Step 2: Get user choice
                    Console.Write("\n👉 Select Payment Method (1-4): ");
                    if (!int.TryParse(Console.ReadLine(), out int choice) ||
                        !Enum.IsDefined(typeof(PaymentType), choice))
                    {
                        Console.WriteLine("❌ Invalid choice! Please select 1-4.");
                        continue;
                    }

                    PaymentType selectedType = (PaymentType)choice;

                    // Step 3: Get payment details based on type
                    PaymentDetails details = GetPaymentDetails(selectedType);

                    // Step 4: Get amount
                    Console.Write("\n💰 Enter Amount (₹): ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal amount) || amount <= 0)
                    {
                        Console.WriteLine("❌ Invalid amount!");
                        continue;
                    }

                    // ⭐ FACTORY PATTERN IN ACTION ⭐
                    // Client code doesn't know about concrete classes
                    // It only works with the interface (IPaymentMethod)
                    IPaymentMethod payment = PaymentFactory.CreatePayment(selectedType, details);

                    Console.WriteLine($"\n✨ Selected: {payment.PaymentMethodName}");

                    // Step 5: Validate and Process Payment
                    if (payment.ValidateDetails())
                    {
                        if (payment.ProcessPayment(amount))
                        {
                            payment.DisplayPaymentInfo();
                        }
                    }

                    // Ask to continue
                    Console.Write("\n🔄 Make another payment? (Y/N): ");
                    string? continueChoice = Console.ReadLine();
                    continueApp = continueChoice?.ToUpper() == "Y";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Error: {ex.Message}");
                }
            }

            DisplayGoodbyeMessage();
        }

        static PaymentDetails GetPaymentDetails(PaymentType type)
        {
            PaymentDetails details = new PaymentDetails();

            Console.WriteLine($"\n📝 Enter {type} Details:");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            switch (type)
            {
                case PaymentType.UPI:
                    Console.Write("   UPI ID (e.g., name@upi): ");
                    details.UpiId = Console.ReadLine();
                    break;

                case PaymentType.InternetBanking:
                    Console.Write("   Account Number: ");
                    details.AccountNumber = Console.ReadLine();
                    Console.Write("   IFSC Code: ");
                    details.IFSCCode = Console.ReadLine();
                    Console.Write("   Account Holder Name: ");
                    details.AccountHolderName = Console.ReadLine();
                    break;

                case PaymentType.CreditCard:
                case PaymentType.DebitCard:
                    Console.Write("   Card Number (16 digits): ");
                    details.CardNumber = Console.ReadLine();
                    Console.Write("   Card Holder Name: ");
                    details.CardHolderName = Console.ReadLine();
                    Console.Write("   Expiry Date (MM/YY): ");
                    details.ExpiryDate = Console.ReadLine();
                    Console.Write("   CVV (3 digits): ");
                    details.CVV = Console.ReadLine();
                    break;
            }

            return details;
        }

        static void DisplayWelcomeBanner()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("║   🏦  WELCOME TO SECURE BANKING PAYMENT SYSTEM  🏦           ║");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("║        ⭐ Using Factory Design Pattern ⭐                     ║");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        }

        static void DisplayGoodbyeMessage()
        {
            Console.WriteLine("\n╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("║         🙏 Thank you for using our Banking System! 🙏       ║");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        }
    }
}