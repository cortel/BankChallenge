namespace BankChallenge.Services.Models.Out.Payment
{
    /// <summary>
    /// The different between the Entity Payment and this Payment is that the Entity payment can sometimes be a God object(bad design)
    /// To avoid bloating query with unnecesary values, we can create a model which is mapped on top of the entity , and we select the necesary values
    /// EX: Entity Payment has 50 props, and we only need an out model with just 4 props.
    /// </summary>
    public class Payment
    {
        public decimal APR { get; set; }

        public decimal MontlyCost { get; set; }

        public decimal TotalPaidInterestRate { get; set; }

        public decimal TotalPaidAdminFees { get; set; }
    }
}
