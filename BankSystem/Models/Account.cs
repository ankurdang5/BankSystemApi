namespace BankSystem.Models
{
    public class Account
    {
        public string Id { get; set; }
        public decimal Balance { get; set; }
        public User User { get; set; }
    }
}
