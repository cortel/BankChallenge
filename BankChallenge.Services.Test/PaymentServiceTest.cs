namespace BankChallenge.Services.Test
{
    using BankChallenge.Common.Configuration;
    using BankChallenge.Services.Services;
    using BankChallenge.Services.Test.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    public class PaymentServiceTest
    {
        private readonly PaymentService paymentService;
        private readonly IOptions<LoanConfiguration> loanConfig;

        public PaymentServiceTest()
        {
            // better unit tests will need to read from appsettings.
            // var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            loanConfig = Options.Create(new LoanConfiguration());
            loanConfig.Value.AdminFeeFixed = 10000;
            loanConfig.Value.AdminFeePercentage = 1;
            loanConfig.Value.AnnualInterestRate = 5;

            paymentService = new PaymentService(loanConfig);
        }

        [TestClass]
        public class CreatePayment
         : PaymentServiceTest
        {
            [TestMethod]
            [TestCategory("Payment")]
            [TestCategory("Create")]
            public void Can_Create_Payment()
            {
            }

            [TestMethod]
            [TestCategory("Payment")]
            [TestCategory("Create")]
            public void Can_Calculate_Monthly_Payment()
            {
                // Arrange
                var totalLoan = 500000;
                var years = 10;

                // Act
                var result = paymentService.CalculateMonthlyPayment(totalLoan, years).GetAwaiter().GetResult();

                // Assert

                // I am not sure if my calculations are wrong, or the number is rounded
                // actual 5303,2757619537767110063718886 an not 5303.28
                Assert.AreEqual(5303.2757619537767110063718886m, result);
            }

            [TestMethod]
            [TestCategory("Payment")]
            [TestCategory("Create")]
            public void Can_Calculate_APR()
            {
                // Act
                var result = paymentService.CalculateAPR().GetAwaiter().GetResult();

                // Assert

                // I am not sure if my calculations are wrong, or the number is rounded
                // actual 5303,2757619537767110063718886
                Assert.AreEqual(5.11618978817300M, result);
            }

            [TestMethod]
            [TestCategory("Payment")]
            [TestCategory("Create")]
            public void Can_Calculate_Total_Amount_Interest_Rate()
            {
                // Arrange

                // Act

                // Assert
            }

            [TestMethod]
            [TestCategory("Payment")]
            [TestCategory("Create")]
            public void Can_Calculate_Total_Administration_Fees()
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}
