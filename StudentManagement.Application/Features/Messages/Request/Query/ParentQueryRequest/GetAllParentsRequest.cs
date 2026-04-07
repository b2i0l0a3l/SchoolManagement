using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.Query.ParentQueryRequest
{
    public class GetAllParentsRequest : IRequest<Result<IEnumerable<ParentModel>>> { }
}
