using BankingFactoryPattern.Enums;
using BankingFactoryPattern.Interfaces;
using BankingFactoryPattern.Models;
using BankingFactoryPattern.PaymentMethods;

namespace BankingFactoryPattern.Factory
{
    /// <summary>
    /// PAYMENT FACTORY - Creates appropriate payment object based on type
    /// This is the HEART of Factory Pattern!
    /// </summary>
    public class PaymentFactory
    {
        /// <summary>
        /// Factory Method - Creates and returns appropriate payment object
        /// Client doesn't need to know about concrete classes!
        /// </summary>
        public static IPaymentMethod CreatePayment(PaymentType paymentType, PaymentDetails details)
        {
            /*
             * ┌─────────────────────────────────────────────────────────┐
             * │                    FACTORY LOGIC                        │
             * │                                                         │
             * │   Input: PaymentType     ───────►  Output: IPayment    │
             * │                                                         │
             * │   UPI            ───────────────►  UPIPayment          │
             * │   InternetBanking ──────────────►  InternetBanking     │
             * │   CreditCard      ──────────────►  CreditCardPayment   │
             * │   DebitCard       ──────────────►  DebitCardPayment    │
             * └─────────────────────────────────────────────────────────┘
             */

            return paymentType switch
            {
                PaymentType.UPI => new UPIPayment(details),

                PaymentType.InternetBanking => new InternetBankingPayment(details),

                PaymentType.CreditCard => new CreditCardPayment(details),

                PaymentType.DebitCard => new DebitCardPayment(details),

                _ => throw new ArgumentException($"Payment type '{paymentType}' is not supported!")
            };
        }

        /// <summary>
        /// Get all available payment methods
        /// </summary>
        public static void DisplayAvailablePaymentMethods()
        {
            Console.WriteLine("\n📋 Available Payment Methods, Please Choose from here :");
            Console.WriteLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");

            foreach (PaymentType type in Enum.GetValues(typeof(PaymentType)))
            {
                Console.WriteLine($"   {(int)type}. {type}");
            }
        }
    }
}
