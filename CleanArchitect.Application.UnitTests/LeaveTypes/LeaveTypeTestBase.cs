using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitect.Application.Contracts.Persistence;
using CleanArchitect.Application.Profiles;
using CleanArchitect.Application.UnitTests.Mocks;
using Moq;

namespace CleanArchitect.Application.UnitTests.LeaveTypes
{
    public abstract class LeaveTypeTestBase
    {
        protected readonly IMapper _mapper;
        protected readonly Mock<ILeaveTypeRepository> _mockRepo;
        public LeaveTypeTestBase()
        {
            _mockRepo = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
    }
}
