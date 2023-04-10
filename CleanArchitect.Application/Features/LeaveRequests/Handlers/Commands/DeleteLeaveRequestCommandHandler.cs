using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitect.Application.Exceptions;
using CleanArchitect.Application.Features.LeaveRequests.Requests.Commands;
using CleanArchitect.Application.Contracts.Persistence;
using CleanArchitect.Domain;
using MediatR;

namespace CleanArchitect.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.Get(request.Id);
            if (leaveRequest == null)
                throw new NotFoundException(nameof(LeaveRequest), request.Id);
            await _leaveRequestRepository.Delete(leaveRequest);
            return Unit.Value;
        }
    }
}
