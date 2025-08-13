using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary.Dto
{
    [NotMapped]
    public class DocumentIncomeInsertDto
    {
        [Required(ErrorMessage = "Не указан номер")]
        [Range(1, int.MaxValue, ErrorMessage = "Должен быть больше 0.")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Не указана дата")]
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);
        public List<DocumentIncomeRowDto>? DocumentIncomeRowDtos { get; set; } = [];


        public DateTime FromDateOnlyToDateTime()
        {
            return Date.ToDateTime(new TimeOnly(12, 0, 0));
        }
    }
}
