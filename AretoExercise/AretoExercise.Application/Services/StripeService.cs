using AretoExercise.Application.Interfaces;
using AretoExercise.Data.Interfaces;
using AretoExercise.Data.Models;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Issuing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AretoExercise.Application.Services
{
    public class StripeService : IStripeService
    {
        private IConfiguration _config;
        private ITransactionsRepository _repository;

        public StripeService(IConfiguration config, ITransactionsRepository repository)
        {
         
            _config = config;
            _repository = repository;

            StripeConfiguration.ApiKey = _config.GetSection("StripeSettings:ApiKey").Value;
        }

        public async Task<bool> Authorize(string paymentId)
        {
            try
            {             
                var service = new AuthorizationService();
                var result = await  service.ApproveAsync(paymentId);
                await _repository.AddTransactionToDb(JsonSerializer.Serialize(result.ToJson()));

                return result.Approved;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Capture(string paymentId,long amount)
        {
            try
            {
                PaymentIntentCaptureOptions options = new PaymentIntentCaptureOptions() {
                    AmountToCapture = amount
                };

                var service = new PaymentIntentService();
                var result = await service.CaptureAsync(paymentId, options);
                await _repository.AddTransactionToDb(JsonSerializer.Serialize(result.ToJson()));

                return result.Status == "succeeded";
            }
            catch (Exception)
            {
                return false;
            }  
        }

        public async Task<bool> CreatePayment(PaymentModel model)
        {
            try
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = model.Amount,
                    Currency = model.Currency,
                    PaymentMethodTypes = model.PaymentMethodTypes
                };
                var service = new PaymentIntentService();
                var result = await service.CreateAsync(options);

                await _repository.AddTransactionToDb(JsonSerializer.Serialize(result.ToJson()));

                return result.Status == "requires_payment_method";
            }
            catch (Exception ex )
            {
                var temp = ex.Message;
                return false;
            }      
        }

        public async Task<bool> Void(string paymentId)
        {
            try
            {
                var service = new PaymentIntentService();
                var result = await service.CancelAsync(paymentId);

                return result.Status == "canceled";
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
