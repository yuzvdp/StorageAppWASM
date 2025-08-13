namespace ClassLibrary.Models
{
    public record DocumentIncome
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public List<DocumentIncomeRow>? DocumentIncomeRows { get; set; } = [];
    }
}
