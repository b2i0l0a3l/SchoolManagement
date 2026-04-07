namespace StudentManagement.Domain.Models
{
    public record ParentModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
