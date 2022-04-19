using AretoExercise.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AretoExercise.Application.Interfaces
{
    public interface IStripeService
    {
        public Task<bool> Capture(string paymentId,long amount);
        public Task<bool> Authorize(string paymentId);
        public Task<bool> Void(string paymentId);
        public Task<bool> CreatePayment(PaymentModel model);
    }
}
