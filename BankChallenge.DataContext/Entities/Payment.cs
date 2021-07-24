 namespace BankChallenge.DataContext.Entities
{
    /// <summary>
    /// This class can be used to be mapped with an ORM -EF , NHibernate, or mini orm like dapper
    /// And afterwards a model can be created on top of it so we dont have to work with Entity directly.(light weight).
    /// In the future, a cache database can be created to temporarily store last 1000 entries from rest endpoint
    /// and then we can simply return previous results, without the need to calculate everything once more.
    /// </summary>
    public class Payment
    {
        public decimal APR { get; set; }

        public decimal MontlyCost { get; set; }

        public decimal TotalPaidInterestRate { get; set; }

        public decimal TotalPaidAdminFees { get; set; }
    }
}
