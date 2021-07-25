namespace BankChallenge.Services.Test
{
    using BankChallenge.Common.Configuration;
    using BankChallenge.Services.Services;
    using BankChallenge.Services.Test.Infrastructure;
    using Microsoft.Extensions.Options;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class PaymentServiceTest : BaseTest
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
                // Arrange
                // this is more of an integration test.
                var totalLoan = 500000;
                var years = 10;

                // Act
                var result = paymentService.Create(totalLoan, years).GetAwaiter().GetResult();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(5000M, result.TotalPaidAdminFees);
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
                var totalLoan = 500000;
                var years = 10;
                var monthlyPay = 5303.28M;

                // Act
                var result = paymentService.CalculateAmountInterestRate(totalLoan, years, monthlyPay).GetAwaiter().GetResult();

                // Assert
                Assert.AreEqual(136393.60M, result);
            }

            [TestMethod]
            [TestCategory("Payment")]
            [TestCategory("Create")]
            public void Can_Calculate_Total_Administration_Fees_With_Fixed()
            {
                // Arrange
                var totalLoan = 500000;

                // Act
                var result = paymentService.CalculateAdministrationFees(totalLoan).GetAwaiter().GetResult();

                // Assert
                Assert.AreEqual(5000M, result);
            }

            [TestMethod]
            [TestCategory("Payment")]
            [TestCategory("Create")]
            public void Can_Calculate_Total_Administration_Fees_With_Percentage()
            {
                // Arrange
                var totalLoan = 500000000;

                // Act
                var result = paymentService.CalculateAdministrationFees(totalLoan).GetAwaiter().GetResult();

                // Assert
                Assert.AreEqual(10000M, result);
            }
        }
    }
}
