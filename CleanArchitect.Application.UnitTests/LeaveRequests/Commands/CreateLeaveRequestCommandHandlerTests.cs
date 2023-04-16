using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.DTOs.LeaveRequest;
using CleanArchitect.Application.Features.LeaveRequests.Handlers.Commands;
using CleanArchitect.Application.Features.LeaveRequests.Requests.Commands;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveRequests.Commands
{
    public class CreateLeaveRequestCommandHandlerTests : LeaveRequestTestBase
    {
        private readonly CreateLeaveRequestCommandHandler _handler;
        private readonly CreateLeaveRequestDto _leaveRequestDto;
        public CreateLeaveRequestCommandHandlerTests()
        {
            _handler = new CreateLeaveRequestCommandHandler(_mockLeaveRequestRepo.Object, _mockLeaveTypeRepo.Object, _mockEmailSender.Object, _mapper);
            _leaveRequestDto = new CreateLeaveRequestDto
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                LeaveTypeId = 1,
                RequestComments = "Test Comments"
            };
        }
        [Fact]
        public async Task CreateLeaveRequest()
        {
            var result = await _handler.Handle(new CreateLeaveRequestCommand() { LeaveRequestDto = _leaveRequestDto }, CancellationToken.None);
            result.ShouldNotBeNull();

            var leaveTypes = await _mockLeaveRequestRepo.Object.GetLeaveRequestsWithDetails();
            leaveTypes.Count.ShouldBe(5);
        }
    }
}
