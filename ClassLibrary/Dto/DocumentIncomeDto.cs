using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary.Dto
{
    [NotMapped]
    public class DocumentIncomeDto
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public IEnumerable<DocumentIncomeRowDto>? DocumentIncomeRowDtos { get; set; }
    }
}
