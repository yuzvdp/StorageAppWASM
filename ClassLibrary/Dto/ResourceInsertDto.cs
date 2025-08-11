using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Dto
{
    public record ResourceInsertDto
    {
        [Required(ErrorMessage = "Необходимо наименование ресурса")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Длина наименования должна быть от {2} до {1} символов")]
        public string? Title { get; set; }
    }
}
