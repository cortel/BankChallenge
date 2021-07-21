namespace BankChallenge.DataContext.Entities
{
    /// <summary>
    /// This class can be used to be mapped with an ORM -EF , NHibernate 
    /// And afterwards a model can be created on top of it so we dont have to work with Entity directly.(light weight)
    /// </summary>
    public class Payment
    {
        public decimal APR { get; set; }

        public decimal MontlyCost { get; set; }

        public decimal TotalPaidInterestRate { get; set; }

        public decimal TotalPaidAdminFees { get; set; }
    }
}
