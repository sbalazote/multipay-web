using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Multipay.DTOs
{
    public class PaymentLinkDTO
    {
        public int AreaCode { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public float TransactionAmount { get; set; }
        public string SellerEmail { get; set; }
    }
}