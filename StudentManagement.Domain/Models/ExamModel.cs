namespace StudentManagement.Domain.Models
{
    public record ExamModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public float MaxScore { get; set; }
    }
}
