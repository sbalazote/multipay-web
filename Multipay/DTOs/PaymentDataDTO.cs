using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multipay.DTOs
{
    public class PaymentDataDTO
    {
        public string CardToken { get; set; }
        public float TransactionAmount { get; set; }
        public string PaymentMethodId { get; set; }
        public string CustomerId { get; set; }
    }
}