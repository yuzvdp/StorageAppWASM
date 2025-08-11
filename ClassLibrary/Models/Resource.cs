namespace ClassLibrary.Models
{
    public record Resource
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
