using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary.Dto
{
    [NotMapped]
    public class UnitInsertDto
    {
        [Required(ErrorMessage = "Необходимо наименование ед. изм.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина наименования должна быть от {2} до {1} символов")]
        public string? Title { get; set; }
    }
}
