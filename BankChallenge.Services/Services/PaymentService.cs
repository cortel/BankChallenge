namespace BankChallenge.Services.Services
{
    using System;
    using System.Threading.Tasks;
    using BankChallenge.Common.Configuration;
    using BankChallenge.Services.Models.Out.Payment;
    using Microsoft.Extensions.Options;

    public class PaymentService : IPaymentService
    {
        private readonly LoanConfiguration loanConfig;

        public PaymentService(IOptions<LoanConfiguration> options)
        {
            this.loanConfig = options.Value;
        }

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

        // A = Payment amount per period
        // P = Initial principal(loan amount)
        // r = Interest rate per period
        // n = Total number of payments or periods
        // The formula for calculating your monthly payment is:
        // A = P { r(1 + r)n} / { (1 + r)n –1}
        public async Task<decimal> CalculateMonthlyPayment(decimal totalLoan,  decimal totalPeriodYears)
        {
            var ratePerPeriod = await CalculateInterestRatePerPeriod(this.loanConfig.AnnualInterestRate);
            var totalPeriods = await CalculateTotalNumberOfPeriods(totalPeriodYears);

            // this might not be the best approach. a proper way would be to extend Math.Pow to take decimals.
            var rateAtPowerOfPeriods = (decimal)Math.Pow((double)(1 + ratePerPeriod), (double)totalPeriods);

            // can't await here. i will await on method call.
            return (totalLoan * ratePerPeriod * rateAtPowerOfPeriods) / (rateAtPowerOfPeriods - 1);
        }

        public async Task<Payment> Create()
        {
            throw new NotImplementedException();
        }

        // annual interest rate converted to percentage and divided by 12 months
        private static async Task<decimal> CalculateInterestRatePerPeriod(decimal annualInterestRate)
        {
            // can't await here. i will await on metho call.
            return (annualInterestRate / 100) / 12;
        }

        // the total number of periods represent the total months in which it is expected that the loan is paid
        private static async Task<decimal> CalculateTotalNumberOfPeriods(decimal years)
        {
            // can't await here. i will await on metho call.
            return years * 12;
        }
    }
}
