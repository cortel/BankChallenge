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

        public async Task<decimal> CalculateAdministrationFees(decimal totalLoan)
        {
            var percentageFee = (totalLoan * loanConfig.AdminFeePercentage) / 100;

            return percentageFee > loanConfig.AdminFeeFixed ? loanConfig.AdminFeeFixed : percentageFee;
        }

        // source https://www.wikihow.com/Find-the-Total-Amount-Paid-in-an-Interest-Rate-Equation
        public async Task<decimal> CalculateAmountInterestRate(decimal totalLoan, decimal totalPeriodYears)
        {
            var nominalRate = CalculateAPR().GetAwaiter().GetResult() ;
            return (totalLoan * ((decimal)Math.Pow((double)(1 + (nominalRate / 100)), (double)totalPeriodYears))) - totalLoan;
        }

        // compounding period = 12 months
        // effective apr formula source https://www.fool.com/knowledge-center/what-are-the-differences-between-apr-ear/
        public async Task<decimal> CalculateAPR()
        {
            var periodicRate = CalculateInterestRatePerPeriod(loanConfig.AnnualInterestRate).GetAwaiter().GetResult();

            return (((decimal)Math.Pow((double)(1 + periodicRate), 12)) - 1) * 100;
        }

        // A = Payment amount per period
        // P = Initial principal(loan amount)
        // r = Interest rate per period
        // n = Total number of payments or periods
        // The formula for calculating your monthly payment is:
        // A = P { r(1 + r)n} / { (1 + r)n –1}
        // refference https://www.vertex42.com/ExcelArticles/amortization-calculation.html
        public async Task<decimal> CalculateMonthlyPayment(decimal totalLoan,  decimal totalPeriodYears)
        {
            var ratePerPeriod = await CalculateInterestRatePerPeriod(this.loanConfig.AnnualInterestRate);
            var totalPeriods = await CalculateTotalNumberOfPeriods(totalPeriodYears);

            // this might not be the best approach. a proper way would be to extend Math.Pow to take decimals.
            var rateAtPowerOfPeriods = (decimal)Math.Pow((double)(1 + ratePerPeriod), (double)totalPeriods);

            // can't await here. i will await on method call.

            // since my result has many many floating numbers, i think is more accurate
            // is the result needed is 5303.28, and not 5303,2757619537767110063718886
            // i can create a separate function that checks if the floating nr is in the range of 3
            // and round it + 1. Again, I do not know bank policies, protocols regards such.
            return (totalLoan * ratePerPeriod * rateAtPowerOfPeriods) / (rateAtPowerOfPeriods - 1);
        }

        public async Task<Payment> Create(decimal totalLoan, decimal totalPeriodYears)
        {
            // the code can be much shorter but i believe is a matter of style
            // i like to see the values i get
            // i coul simply return a new payment created directly with called functions
            var apr = await CalculateAPR();
            var monthlyCost = await CalculateMonthlyPayment(totalLoan, totalPeriodYears);
            var totalPaidInterestRate = await CalculateAmountInterestRate(totalLoan, totalPeriodYears);
            var totalPaidAdminFees = await CalculateAdministrationFees(totalLoan);

            var payment = new Payment()
            {
                APR = apr,
                MontlyCost = monthlyCost,
                TotalPaidAdminFees = totalPaidAdminFees,
                TotalPaidInterestRate = totalPaidInterestRate,
            };

            return payment;
        }

        // annual interest rate converted to percentage and divided by 12 months
        // also known as periodicRate
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
