namespace BankSystem.Models
{
    public class CreateAccountRequest
    {
        public string? Id { get; set; }
        public decimal Balance { get; set; }
        public string? Userid { get; set; }
        public string? UserName { get; set; }
        public string? PanCard { get; set; }
    }
}
