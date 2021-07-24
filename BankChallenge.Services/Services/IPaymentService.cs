namespace BankChallenge.Services.Services
{
    using System.Threading.Tasks;
    using BankChallenge.Services.Models.Out.Payment;

    public interface IPaymentService
    {
        Task<Payment> Create(decimal totalLoan, decimal totalPeriodYears);

        Task<decimal> CalculateAPR();

        Task<decimal> CalculateMonthlyPayment(decimal totalLoan, decimal totalPeriodYears);

        Task<decimal> CalculateAmountInterestRate(decimal totalLoan, decimal totalPeriodYears, decimal monthlyPayment);

        Task<decimal> CalculateAdministrationFees(decimal totalLoan);
    }
}
