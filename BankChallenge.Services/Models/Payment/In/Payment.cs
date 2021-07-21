namespace BankChallenge.Services.Models.Payment.In
{
   public class Payment
    {
        public decimal LoanAmount { get; set; }

        /// <summary>
        /// Gets or sets DurationOfLoan in Years.
        /// </summary>
        public int DurationOfLoan { get; set; }
    }
}
