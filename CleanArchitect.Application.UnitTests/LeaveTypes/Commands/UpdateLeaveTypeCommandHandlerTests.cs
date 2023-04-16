using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.DTOs.LeaveType;
using CleanArchitect.Application.Exceptions;
using CleanArchitect.Application.Features.LeaveTypes.Handlers.Commands;
using CleanArchitect.Application.Features.LeaveTypes.Requests.Commands;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveTypes.Commands
{
    public class UpdateLeaveTypeCommandHandlerTests : LeaveTypeTestBase
    {
        private readonly UpdateLeaveTypeCommandHandler _handler;
        private readonly LeaveTypeDto _leaveTypeDto;
        public UpdateLeaveTypeCommandHandlerTests()
        {
            _handler = new UpdateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);
            _leaveTypeDto = new LeaveTypeDto()
            {
                Id = 1,
                Name = "Test Vaccationnnnnn",
                DefaultDays = 10
            };
        }
        [Fact]
        public async Task UpdateLeaveType()
        {
            await _handler.Handle(new UpdateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);
            var leaveType = await _mockRepo.Object.Get(_leaveTypeDto.Id);
            leaveType.ShouldNotBeNull();
            leaveType.Name.ShouldBeEquivalentTo(_leaveTypeDto.Name);
        }
        [Fact]
        public async Task UpdateLeaveType_ValidationException()
        {
            _leaveTypeDto.Name = "";
            var ex = await Should.ThrowAsync<ValidationException>(async () =>
            {
                await _handler.Handle(new UpdateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);
            });
            ex.ShouldNotBeNull();

            var leaveType = await _mockRepo.Object.Get(_leaveTypeDto.Id);
            leaveType.ShouldNotBeNull();
            leaveType.Name.ShouldNotBeSameAs(_leaveTypeDto.Name);
        }
        [Fact]
        public async Task UpdateLeaveType_NotFoundException()
        {
            _leaveTypeDto.Id = 99;
            var ex = await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await _handler.Handle(new UpdateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);
            });
            ex.ShouldNotBeNull();
        }
    }
}
