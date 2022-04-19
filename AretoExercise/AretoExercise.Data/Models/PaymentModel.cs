using System;
using System.Collections.Generic;
using System.Text;

namespace AretoExercise.Data.Models
{
    public class PaymentModel
    {
        public int Amount { get; set; }
        public string Currency { get; set; }
        public List<string> PaymentMethodTypes { get; set; }
    }
}
