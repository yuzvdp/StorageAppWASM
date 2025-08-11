namespace ClassLibrary.Models
{
    public record DocumentOutcome
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public bool IsConfirmed { get; set; } = false;
    }
}
