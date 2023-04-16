using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.Exceptions;
using CleanArchitect.Application.Features.LeaveRequests.Handlers.Commands;
using CleanArchitect.Application.Features.LeaveRequests.Requests.Commands;
using Moq;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveRequests.Commands
{
    public class DeleteLeaveRequestCommandHandlerTests : LeaveRequestTestBase
    {
        private readonly DeleteLeaveRequestCommandHandler _handler;
        public DeleteLeaveRequestCommandHandlerTests()
        {
            _handler = new DeleteLeaveRequestCommandHandler(_mockLeaveRequestRepo.Object, _mapper);
        }
        [Fact]
        public async Task DeleteLeaveRequest()
        {
            var leaveRequests = await _mockLeaveRequestRepo.Object.GetAll();
            await _handler.Handle(new DeleteLeaveRequestCommand() { Id = 1 }, CancellationToken.None);

            leaveRequests = await _mockLeaveRequestRepo.Object.GetAll();
            leaveRequests.Count.ShouldBe(3);
        }
        [Fact]
        public async Task DeleteLeaveRequest_NotFoundException()
        {
            var leaveRequests = await _mockLeaveRequestRepo.Object.GetAll();
            var ex = await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await _handler.Handle(new DeleteLeaveRequestCommand() { Id = 5 }, CancellationToken.None);
            });
            ex.ShouldNotBeNull();
        }
    }
}
