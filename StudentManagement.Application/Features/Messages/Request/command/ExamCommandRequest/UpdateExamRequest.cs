using MediatR;
using StudentManagement.Domain.Common;

namespace StudentManagement.Application.Features.Messages.Request.command.ExamCommandRequest
{
    public class UpdateExamRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public float MaxScore { get; set; }
    }
}
