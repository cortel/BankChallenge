namespace BankChallenge.Services.Test
{
    using BankChallenge.Common.Configuration;
    using BankChallenge.Services.Services;
    using BankChallenge.Services.Test.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class PaymentServiceTest
    {
        private readonly PaymentService paymentService;
        private readonly IOptions<LoanConfiguration> loanConfig;

        public PaymentServiceTest()
        {
            // to compare if values are correctly passed from json
           var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

           loanConfig = Options.Create(new LoanConfiguration());
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
                var result = paymentService.CalculateMonthlyPayment(totalLoan, years);

                // Assert
                Assert.AreEqual(5303.28, result);
            }

            [TestMethod]
            [TestCategory("Payment")]
            [TestCategory("Create")]
            public void Can_Calculate_APR()
            {
                // Arrange

                // Act

                // Assert
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
