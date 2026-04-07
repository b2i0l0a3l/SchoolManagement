using MediatR;
using StudentManagement.Domain.Common;

namespace StudentManagement.Application.Features.Messages.Request.command.ExamCommandRequest
{
    public class DeleteExamRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
