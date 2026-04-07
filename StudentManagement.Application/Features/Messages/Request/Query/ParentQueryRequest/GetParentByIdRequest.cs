using MediatR;
using StudentManagement.Domain.Common;
using StudentManagement.Domain.Models;

namespace StudentManagement.Application.Features.Messages.Request.Query.ParentQueryRequest
{
    public class GetParentByIdRequest : IRequest<Result<ParentModel>>
    {
        public int ParentId { get; set; }
    }
}
