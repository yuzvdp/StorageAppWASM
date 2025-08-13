namespace ClassLibrary.Models
{
    public record Balance
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public Resource? Resource { get; set; }
        public int UnitId { get; set; }
        public Unit? Unit { get; set; }
        public double Count { get; set; }

        public void IncreaseCount(double count)
        {
            Count += count;
        }

        public void DecreaseCount(double count)
        {
            Count -= count;
        }
    }
}
