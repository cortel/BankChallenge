namespace BankChallenge.Services.Services
{
    using System.Threading.Tasks;
    using BankChallenge.Services.Models.Out.Payment;

    public interface IPaymentService
    {
        Task<Payment> Create();
    }
}
