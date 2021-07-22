namespace BankChallenge.Services.Services
{
    using System;
    using System.Threading.Tasks;
    using BankChallenge.Services.Models.Out.Payment;

    public class PaymentService : IPaymentService
    {
        public Task<decimal> CalculateAdministrationFees()
        {
            throw new NotImplementedException();
        }

        public Task<decimal> CalculateAmountInterestRate()
        {
            throw new NotImplementedException();
        }

        public Task<decimal> CalculateAPR()
        {
            throw new NotImplementedException();
        }

        public Task<decimal> CalculateMonthlyPayment()
        {
            throw new NotImplementedException();
        }

        public async Task<Payment> Create()
        {
            throw new NotImplementedException();
        }
    }
}
