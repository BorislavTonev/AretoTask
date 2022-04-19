using AretoExercise.Application.Interfaces;
using AretoExercise.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AretoExercise.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {

        private IStripeService _stripeService;

        public PaymentsController(IStripeService stripeService)
        {
            _stripeService = stripeService;
        }

        [HttpPost]
        [Route("AuthorizePayment")]
        [Authorize]
        public async Task<IActionResult> AuthorizePayment(string paymentId)
        {
            try
            {
                var status = await _stripeService.Authorize(paymentId);

                if (status)
                {
                    return Ok("Payment authorization is successfull.");
                }
                else
                {
                    return BadRequest("Payment authorization failed!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CapturePayment")]
        [Authorize]
        public async Task<IActionResult> CapturePayment([FromBody] CaptureModel model)
        {
            try
            {
                var status = await _stripeService.Capture(model.PaymentId,model.Amount);

                if (status)
                {
                    return Ok("Payment capture is successfull");
                }
                else
                {
                    return BadRequest("Payment capture failed!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("VoidPayment")]
        [Authorize]
        public async Task<IActionResult> VoidPayment(string paymentId)
        {
            try
            {
                var status = await _stripeService.Void(paymentId);

                if (status)
                {
                    return Ok("Payment cancelation is successfull");
                }
                else
                {
                    return BadRequest("Payment cancelation failed!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreatePayment")]
        [Authorize]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentModel model)
        {
            try
            {
                var status = await _stripeService.CreatePayment(model);

                if (status)
                {
                    return Ok("Payment creation is successfull");
                }
                else
                {
                    return BadRequest("Payment creation failed!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }     
        }

    }
}
