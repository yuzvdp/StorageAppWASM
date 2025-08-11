namespace ClassLibrary.Models
{
    public record Client
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
