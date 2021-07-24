using BankChallenge.Services.Models.Out.Payment;
using BankChallenge.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class PaymentServiceController : ControllerBase
    {
        private readonly IPaymentService paymentService;

        public PaymentServiceController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpGet, Route("api/v{version}/payment/create/{totalLoan}/{totalYears}")]
        [SwaggerResponse(200, "Create Payment was successfully retrieved.", typeof(Payment))]
        public async Task<ActionResult<Payment>> GetPayment(decimal totalLoan, decimal totalYears)
        {
            var result = await paymentService.Create(totalLoan, totalYears);
            return result;
        }
    }
}
