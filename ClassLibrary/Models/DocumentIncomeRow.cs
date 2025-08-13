namespace ClassLibrary.Models
{
    public record DocumentIncomeRow
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public Resource? Resource { get; set; }
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }
        public int DocumentIncomeId { get; set; }
        public DocumentIncome? DocumentIncome { get; set; }
        public double Count { get; set; }
    }
}
