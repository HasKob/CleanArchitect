using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitect.Application.Contracts.Persistence;
using CleanArchitect.Application.DTOs.LeaveType;
using CleanArchitect.Application.Exceptions;
using CleanArchitect.Application.Features.LeaveTypes.Handlers.Queries;
using CleanArchitect.Application.Features.LeaveTypes.Requests.Queries;
using Moq;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveTypes.Queries
{
    public class GetLeaveTypeDetailsRequestHandlerTests : LeaveTypeTestBase
    {
        private readonly GetLeaveTypeDetailRequestHandler _handler;
        public GetLeaveTypeDetailsRequestHandlerTests()
        {
            _handler = new GetLeaveTypeDetailRequestHandler(_mockRepo.Object, _mapper);
        }
        [Fact]
        public async Task GetLeaveTypeDetails()
        {
            var result = await _handler.Handle(new GetLeaveTypeDetailRequest() { Id = 1 }, CancellationToken.None);
            result.ShouldBeOfType<LeaveTypeDto>();
        }
        [Fact]
        public async Task GetLeaveTypeDetails_ThrowsExceptionWhenLeaveTypeNotFound()
        {
            var ex = await Should.ThrowAsync<NotFoundException>(async () =>
                await _handler.Handle(new GetLeaveTypeDetailRequest() { Id = 99 }, CancellationToken.None)
            );
            ex.ShouldNotBeNull();
        }
    }
}
