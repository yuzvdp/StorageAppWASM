namespace ClassLibrary.Dto
{
    public record ResourceUpdateDto
    {
        public string? Title { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
