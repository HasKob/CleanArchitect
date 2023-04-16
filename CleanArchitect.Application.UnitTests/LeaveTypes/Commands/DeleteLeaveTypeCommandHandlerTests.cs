using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.Exceptions;
using CleanArchitect.Application.Features.LeaveTypes.Handlers.Commands;
using CleanArchitect.Application.Features.LeaveTypes.Requests.Commands;
using CleanArchitect.Domain;
using Moq;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveTypes.Commands
{
    public class DeleteLeaveTypeCommandHandlerTests : LeaveTypeTestBase
    {
        private readonly DeleteLeaveTypeCommandHandler _handler;
        private readonly int _leaveTypeId;
        public DeleteLeaveTypeCommandHandlerTests()
        {
            _handler = new DeleteLeaveTypeCommandHandler(_mockRepo.Object, _mapper);
            _leaveTypeId = 1;
        }
        [Fact]
        public async Task DeleteLeaveType()
        {
            var leaveTypes = await _mockRepo.Object.GetAll();
            await _handler.Handle(new DeleteLeaveTypeCommand() { Id = _leaveTypeId }, CancellationToken.None);

            leaveTypes = await _mockRepo.Object.GetAll();
            leaveTypes.Count.ShouldBe(2);
        }
        [Fact]
        public async Task DeleteLeaveType_NotFoundException()
        {
            var ex = await Should.ThrowAsync<NotFoundException>(async () =>
            {
                await _handler.Handle(new DeleteLeaveTypeCommand() { Id= 99 }, CancellationToken.None);
            });
            ex.ShouldNotBeNull();
        }
    }
}
