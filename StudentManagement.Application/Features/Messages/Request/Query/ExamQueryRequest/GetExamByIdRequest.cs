using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.Query.ExamQueryRequest
{
    public class GetExamByIdRequest : IRequest<Result<ExamModel>>
    {
        public int ExamId { get; set; }
    }
}
