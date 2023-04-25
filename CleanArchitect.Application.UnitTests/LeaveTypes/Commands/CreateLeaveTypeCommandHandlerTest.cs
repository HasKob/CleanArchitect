using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitect.Application.Contracts.Persistence;
using CleanArchitect.Application.DTOs.LeaveType;
using CleanArchitect.Application.Exceptions;
using CleanArchitect.Application.Features.LeaveTypes.Handlers.Commands;
using CleanArchitect.Application.Features.LeaveTypes.Requests.Commands;
using CleanArchitect.Application.Profiles;
using CleanArchitect.Application.Responses;
using CleanArchitect.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTest : LeaveTypeTestBase
    {
        private readonly CreateLeaveTypeDto _leaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _handler;

        public CreateLeaveTypeCommandHandlerTest()
        {

            _leaveTypeDto = new CreateLeaveTypeDto
            {
                Name = "Test Leave Type Dto",
                DefaultDays = 15
            };

            _handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object, _mapper);
        }
        [Fact]
        public async Task Valid_LeaveType_Added()
        {
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();

            var leaveTypes = await _mockRepo.Object.GetAll();
            leaveTypes.Count.ShouldBe(4);
        }
        [Fact]
        public async Task InValid_LeaveType_Added()
        {
            _leaveTypeDto.DefaultDays = -1;
            var result = await _handler.Handle(new CreateLeaveTypeCommand() { LeaveTypeDto = _leaveTypeDto }, CancellationToken.None);
            result.ShouldBeOfType<BaseCommandResponse>();
            var leaveTypes = await _mockRepo.Object.GetAll();
            leaveTypes.Count.ShouldBe(3);
        }
    }
}
