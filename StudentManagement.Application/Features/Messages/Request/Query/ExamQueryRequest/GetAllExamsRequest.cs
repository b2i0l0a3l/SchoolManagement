using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.Query.ExamQueryRequest
{
    public class GetAllExamsRequest : IRequest<Result<IEnumerable<ExamModel>>> { }
}
