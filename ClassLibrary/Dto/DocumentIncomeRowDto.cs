using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary.Dto
{
    [NotMapped]
    public class DocumentIncomeRowDto
    {
        public int Id { get; set; }
        public int ResourceId { get; set; }
        public string? ResourceTitle { get; set; }
        public int UnitId { get; set; }
        public string? UnitTitle { get; set; }
        public int DocumentIncomeId { get; set; }

        [Required(ErrorMessage = "Не указано количество")]
        [Range(1, int.MaxValue, ErrorMessage = "Должен быть больше 0.")]
        public double Count { get; set; }
    }
}
