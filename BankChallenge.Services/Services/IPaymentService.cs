namespace BankChallenge.Services.Services
{
    using System.Threading.Tasks;
    using BankChallenge.Services.Models.Out.Payment;

    public interface IPaymentService
    {
        Task<Payment> Create();

        Task<decimal> CalculateAPR();

        Task<decimal> CalculateMonthlyPayment(decimal totalLoan, decimal totalPeriodYears);

        Task<decimal> CalculateAmountInterestRate();

        Task<decimal> CalculateAdministrationFees();
    }
}
