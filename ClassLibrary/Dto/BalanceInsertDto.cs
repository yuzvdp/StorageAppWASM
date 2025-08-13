using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassLibrary.Dto
{
    [NotMapped]
    public class BalanceInsertDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходим ресурс")]
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Необходима ед. изм.")]
        public int UnitId { get; set; }

        public double Count { get; set; }
    }
}
