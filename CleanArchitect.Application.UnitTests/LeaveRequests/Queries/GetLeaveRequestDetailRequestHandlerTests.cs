using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.DTOs.LeaveRequest;
using CleanArchitect.Application.Exceptions;
using CleanArchitect.Application.Features.LeaveRequests.Handlers.Queries;
using CleanArchitect.Application.Features.LeaveRequests.Requests.Queries;
using CleanArchitect.Domain;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveRequests.Queries
{
    public class GetLeaveRequestDetailRequestHandlerTests : LeaveRequestTestBase
    {
        private readonly GetLeaveRequestDetailRequestHandler _handler;
        private readonly int _leaveRequestId;
        public GetLeaveRequestDetailRequestHandlerTests()
        {
            _handler = new GetLeaveRequestDetailRequestHandler(_mockLeaveRequestRepo.Object, _mapper);
            _leaveRequestId = 1;
        }
        [Fact]
        public async Task GetLeaveRequestDetails()
        {
            var result = await _handler.Handle(new GetLeaveRequestDetailRequest() { Id = _leaveRequestId }, CancellationToken.None);
            result.ShouldBeOfType<LeaveRequestDto>();
        }
        [Fact]
        public async Task GetLeaveRequestDetails_NotFoundException()
        {
            var ex = await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await _handler.Handle(new GetLeaveRequestDetailRequest() { Id = 99 }, CancellationToken.None);
            });
            ex.ShouldNotBeNull();
        }
    }
}
