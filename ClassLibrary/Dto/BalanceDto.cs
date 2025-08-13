using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary.Dto
{
    [NotMapped]
    public class BalanceDto
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string? ResourceTitle { get; set; }
        public int UnitId { get; set; }
        public string? UnitTitle { get; set; }
        public double Count { get; set; }
    }
}
