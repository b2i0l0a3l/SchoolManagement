using MediatR;
using StudentManagement.Domain.Common;

namespace StudentManagement.Application.Features.Messages.Request.command.ParentCommandRequest
{
    public class DeleteParentRequest : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
