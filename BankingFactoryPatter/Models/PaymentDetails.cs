using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingFactoryPattern.Models
{
    public class PaymentDetails
    {
        // For UPI
        public string? UpiId { get; set; }

        // For Internet Banking
        public string? AccountNumber { get; set; }
        public string? IFSCCode { get; set; }
        public string? AccountHolderName { get; set; }

        // For Cards
        public string? CardNumber { get; set; }
        public string? CardHolderName { get; set; }
        public string? ExpiryDate { get; set; }
        public string? CVV { get; set; }
    }
}
