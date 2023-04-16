using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitect.Application.Contracts.Persistence;
using CleanArchitect.Application.DTOs.LeaveType;
using CleanArchitect.Application.Features.LeaveTypes.Handlers.Queries;
using CleanArchitect.Application.Features.LeaveTypes.Requests.Queries;
using CleanArchitect.Application.Profiles;
using CleanArchitect.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace CleanArchitect.Application.UnitTests.LeaveTypes.Queries
{
    public class GetLeaveTypeListRequestHandlerTests : LeaveTypeTestBase
    {
        public GetLeaveTypeListRequestHandlerTests()
        {
        }
        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            var handler = new GetLeaveTypeListRequestHandler(_mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetLeaveTypeListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count.ShouldBe(3);
        }
    }
}
