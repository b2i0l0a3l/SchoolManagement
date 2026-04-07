using StudentManagement.Domain.enums;

namespace StudentManagement.Domain.Models
{
    public record AttendanceModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatus Status { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}
