using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.command.ExamCommandRequest
{
    public class AddNewExamRequest : IRequest<Result<ExamModel>>
    {
        public string Title { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int SubjectId { get; set; }
        public float MaxScore { get; set; } = 100;
    }
}
