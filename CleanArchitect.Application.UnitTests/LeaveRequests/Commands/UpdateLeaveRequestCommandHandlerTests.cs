using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.DTOs.LeaveRequest;
using CleanArchitect.Application.Exceptions;
using CleanArchitect.Application.Features.LeaveRequests.Handlers.Commands;
using CleanArchitect.Application.Features.LeaveRequests.Requests.Commands;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveRequests.Commands
{
    public class UpdateLeaveRequestCommandHandlerTests : LeaveRequestTestBase
    {
        private readonly UpdateLeaveRequestCommandHandler _handler;
        private readonly UpdateLeaveRequestDto _updateLeaveRequestDto;
        private readonly ChangeLeaveRequestApprovalDto _changeLeaveRequestApprovalDto;
        public UpdateLeaveRequestCommandHandlerTests()
        {
            _handler = new UpdateLeaveRequestCommandHandler(_mockLeaveRequestRepo.Object, _mockLeaveTypeRepo.Object, _mapper);
            _updateLeaveRequestDto = new UpdateLeaveRequestDto()
            {
                Id = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Canceled = false,
                LeaveTypeId = 1,
                RequestComments = "Test Comment Update"
            };
            _changeLeaveRequestApprovalDto = new ChangeLeaveRequestApprovalDto()
            {
                Id = 2,
                Approved = true
            };
        }
        [Fact]
        public async Task UpdateLeaveRequest()
        {
            await _handler.Handle(new UpdateLeaveRequestCommand() { Id = 1, LeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);
            var leaveTypes = await _mockLeaveRequestRepo.Object.GetLeaveRequestsWithDetails();

        }
        [Fact]
        public async Task UpdateLeaveRequest_ValidationException()
        {
            _updateLeaveRequestDto.EndDate = new DateTime(2022, 12, 30);
            var ex = await Should.ThrowAsync<ValidationException>(async () =>
            {
                await _handler.Handle(new UpdateLeaveRequestCommand() { Id = 1, LeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);
            });
        }
        [Fact]
        public async Task UpdateLeaveRequest_LeaveRequestNotFoundException()
        {
            var ex = await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await _handler.Handle(new UpdateLeaveRequestCommand() { Id = 5, LeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);
            });
        }
        [Fact]
        public async Task UpdateLeaveRequest_LeaveTypeNotFoundException()
        {
            _updateLeaveRequestDto.LeaveTypeId = 5;
            var ex = await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await _handler.Handle(new UpdateLeaveRequestCommand() { Id = 2, LeaveRequestDto = _updateLeaveRequestDto }, CancellationToken.None);
            });
        }
        [Fact]
        public async Task ChangeApprovalStatus()
        {
            await _handler.Handle(new UpdateLeaveRequestCommand() { Id = 2, ChangeLeaveRequestApprovalDto = _changeLeaveRequestApprovalDto }, CancellationToken.None);
            var leaveType = await _mockLeaveRequestRepo.Object.Get(_changeLeaveRequestApprovalDto.Id);
            leaveType.Approved.ShouldBe(_changeLeaveRequestApprovalDto.Approved);

        }
        [Fact]
        public async Task ChangeApprovalStatus_NotFoundException()
        {
            var ex = await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await _handler.Handle(new UpdateLeaveRequestCommand() { Id = 5, ChangeLeaveRequestApprovalDto = _changeLeaveRequestApprovalDto }, CancellationToken.None);
            });
        }
    }
}
