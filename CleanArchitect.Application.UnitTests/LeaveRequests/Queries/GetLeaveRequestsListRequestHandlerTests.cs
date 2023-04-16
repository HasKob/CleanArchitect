using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.DTOs.LeaveRequest;
using CleanArchitect.Application.Features.LeaveRequests.Handlers.Queries;
using CleanArchitect.Application.Features.LeaveRequests.Requests.Queries;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveRequests.Queries
{
    public class GetLeaveRequestsListRequestHandlerTests : LeaveRequestTestBase
    {
        private readonly GetLeaveRequestListRequestHandler _handler;
        public GetLeaveRequestsListRequestHandlerTests()
        {
            _handler = new GetLeaveRequestListRequestHandler(_mockLeaveRequestRepo.Object, _mapper);
        }
        [Fact]
        public async Task GetLeaveRequests()
        {
            var result = await _handler.Handle(new GetLeaveRequestListRequest(), CancellationToken.None);
            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<LeaveRequestListDto>>();
            result.Count.ShouldBe(4);
        }
    }
}
