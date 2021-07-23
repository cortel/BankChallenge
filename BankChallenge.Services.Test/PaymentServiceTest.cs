namespace BankChallenge.Services.Test
{
    using BankChallenge.Services.Services;
    using BankChallenge.Services.Test.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class PaymentServiceTest : BaseTest
    {
        private readonly PaymentService paymentService;

        public PaymentServiceTest()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            this.paymentService = new PaymentService();
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

                // Act

                // Assert
            }

            [TestMethod]
            [TestCategory("Payment")]
            [TestCategory("Create")]
            public void Can_Calculate_Monthly_Payment()
            {
                // Arrange

                // Act

                // Assert
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
